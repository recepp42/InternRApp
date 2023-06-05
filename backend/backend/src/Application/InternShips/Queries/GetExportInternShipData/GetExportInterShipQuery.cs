using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Resources;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Common;
using backend.Application.InternShips.Queries.GetAllInternShips;
using backend.Application.InternShips.Queries.GetInternShipById;
using backend.Application.Units.Common;
using backend.Domain.Entities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace backend.Application.InternShips.Queries.GetExportInternShipData;
public class GetExportInterShipQuery : IRequest<List<UnitExportDto>>
{
    public List<int> UnitIds { get; set; }
    public string SchoolYear { get; set; }
    public int LanguageId { get; set; }
}

public class GetExportInterShipQueryHandler : IRequestHandler<GetExportInterShipQuery, List<UnitExportDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _iMapper;

    public GetExportInterShipQueryHandler(IApplicationDbContext dbContext, IMapper iMapper)
    {
        _dbContext = dbContext;
        _iMapper = iMapper;
    }

    public async Task<List<UnitExportDto>> Handle(GetExportInterShipQuery request, CancellationToken cancellationToken)
    {
        var units = await _dbContext.Departments
            .Include(dept => dept.Internships)
            .ThenInclude(intrshp => intrshp.Locations)
            .Where(unit => (request.UnitIds == null || request.UnitIds.Count == 0 || request.UnitIds.Contains(unit.Id))
            && unit.Internships.Any(intrShps => intrShps.Translations.Any(trnsl => request.LanguageId == trnsl.LanguageId))
            && unit.Internships.Any(intrShips => request.SchoolYear == intrShips.SchoolYear))

            .Select(exportDto => new UnitExportDto()
            {
                Name = exportDto.Name,
                PrefaceDto = exportDto.PrefaceTranslations.Where(unit => unit.LanguageId == request.LanguageId)
                .Select(unit => new PrefaceTranslationDto()
                {
                    TranslationId = unit.Id,
                    Content = unit.Content,
                    Language = _iMapper.Map<LanguageListDto>(unit.Language),
                }).Single(),
                InternShipsDtos = exportDto.Internships
                .Where(intrshp => intrshp.Translations.Any(trnsl => request.LanguageId == trnsl.LanguageId) //check
                && request.SchoolYear == intrshp.SchoolYear)
                .Select(intrshp => new InternShipExportDto()
                {
                    SchoolYear = intrshp.SchoolYear,
                    Locations = intrshp.Locations.Select(loc => new LocationDto()
                    {
                        City = loc.City,
                        Housenumber = loc.HouseNumber,
                        Streetname = loc.StreetName,
                        Zipcode = loc.ZipCode
                    }).ToList(),
                    TrainingType = intrshp.RequiredTrainingType,
                    Translation = intrshp.Translations.Where(trnsl => trnsl.LanguageId == request.LanguageId) //check
                    .Select(trnslDto => new TranslationDto()
                    {
                        TranslationId = trnslDto.Id,
                        TitleContent = trnslDto.TitleContent,
                        Comment = trnslDto.Comment,
                        Description = trnslDto.Description,
                        KnowledgeToDevelop = trnslDto.KnowledgeToDevelop,
                        NeededKnowledge = trnslDto.NeededKnowledge
                    }).First(),
                    Languages = intrshp.Translations.Select(x => new LanguageListDto()
                    {
                        Id = x.Id,
                        Code = x.Language.Code,
                        Name = x.Language.Name,
                    }).ToList()
                }).ToList()
            }).ToListAsync();


        foreach (var item in units)
        {

        }

        //return new List<UnitExportDto>() { };
        return units;
    }
}

public class Exporting
{
    private readonly IApplicationDbContext _dbContext;
    public Exporting(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string GenerateWordDocFilePath(List<UnitExportDto> unitExportList, InternshipExportRequestDto requestDto)
    {
        //env variabel uitlezen
        string environmentVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string templatePath;
        string resultPath;

        if (environmentVar == "Development")
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            templatePath = Path.Combine(sCurrentDirectory, @"..\..\..\wwwroot\lib\template.docx");
            resultPath = Path.Combine(sCurrentDirectory, $@"..\..\..\wwwroot\lib\ExportFiles\internships_{Guid.NewGuid()}.docx");
        }
        else
        {
            templatePath = @"/home/site/wwwroot/wwwroot/lib/template.docx";
            resultPath = $@"/home/site/wwwroot/wwwroot/lib/ExportFiles/internships_{Guid.NewGuid()}.docx";
        }

        using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(templatePath, false))
        {
            var body = document.MainDocumentPart!.Document.Body;
            var allElements = body!.Elements().ToList();
            int altChunckId = 0;

            //Resource content
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == "WebUI").ToList();
            var langCode = _dbContext.Languages.Where(lang => lang.Id == requestDto.LanguageId).Single().Code;
            var webAssembly = Assembly.LoadFile(@$"{assemblies[0].Location}");
            ResourceManager rm;
            var check = false;

            foreach (var resourceItem in webAssembly.GetManifestResourceNames())
            {
                if (resourceItem == $"WebUI.Resources.{langCode}.resources")
                {
                    check = true;
                    continue;
                }
            }
            rm = check ? new ResourceManager($"WebUI.Resources.{langCode}", webAssembly) : new ResourceManager("WebUI.Resources.en", webAssembly);

            Dictionary<string, string> commonReplacements = new Dictionary<string, string>()
                 {
                     {"TEMPLATE_TITLE", rm.GetString("TEMPLATE_TITLE")},
                     {"TEMPLATE_YEAR", requestDto.SchoolYear},
                     {"COMPANY_TITLE", rm.GetString("COMPANY_TITLE")},
                     {"COMPANY_DESCRIPTION", rm.GetString("COMPANY_DESCRIPTION")},
                     {"COMPANY_VISION_TITLE", rm.GetString("COMPANY_VISION_TITLE")},
                     {"COMPANY_VISION_DESCRIPTION", rm.GetString("COMPANY_VISION_DESCRIPTION")},
                     {"COMPANY_VALUES_TITLE", rm.GetString("COMPANY_VALUES_TITLE")},
                     {"COMPANY_VALUES_DESCRIPTION", rm.GetString("COMPANY_VALUES_DESCRIPTION")},
                     {"ACADEMICT_TITLE", rm.GetString("ACADEMICT_TITLE")},
                     {"ACADEMICT_DESCRIPTION", rm.GetString("ACADEMICT_DESCRIPTION")},
                 };

            Replace(document, ref allElements, commonReplacements, ref altChunckId);

            ExtractParagraphs(allElements, "UNIT_TITLE", "UNIT_END", out var unitStartPosition, out var templateUnitParagraphs);

            allElements.RemoveRange(unitStartPosition, templateUnitParagraphs.Count);

            InsertUnitParagraphs(document, ref allElements, unitExportList, templateUnitParagraphs, unitStartPosition, ref altChunckId, rm);

            List<string> toDeleteStrings = new List<string>() { "INTERNSHIP_END", "UNIT_END" };
            DeleteParagraphs(ref allElements, toDeleteStrings);

            //update index table 
            document.MainDocumentPart!.DocumentSettingsPart!.Settings.Append(new UpdateFieldsOnOpen() { Val = true }); //dit update de velden maar toont nog steeds een update dialog

            body.RemoveAllChildren();

            //insert allElements in body again
            for (int i = 0; i < allElements.Count(); i++)
            {
                body.InsertAt(allElements[i], i);
            }

            //remove old content table
            RemoveSdt(body);

            document.SaveAs(resultPath).Dispose();
        }

        return resultPath;

    }
    public string GetCombinedText(IEnumerable<Text> textElements)
    {
        //plak alle text elementen aan elkaar die bij elke paragraaf horen 
        return string.Join("", textElements.Select(t => t.Text)); // IN TERN SHIP TI TLE -> INTERNSHIP_TITLE
    }
    public List<T> Reversed<T>(IList<T> list)
    {
        return Enumerable
            .Range(1, list.Count())
            .Select(i => list[list.Count() - i])
            .ToList();
    }
    public List<OpenXmlElement> CloneELements(List<OpenXmlElement> elements)
    {
        return elements
            .Select(element => element.CloneNode(true))
            .ToList();
    }

    public void Replace(WordprocessingDocument document, ref List<OpenXmlElement> openXmlElements, Dictionary<string, string> replacements, ref int chunkId)
    {
        var toReplace = new Dictionary<OpenXmlElement, OpenXmlElement>();

        foreach (var e in openXmlElements)
        {
            if (e is Paragraph)
            {
                var p = e as Paragraph;

                var textElements = p.Elements<Run>()
                                   .SelectMany(r => r.Elements<Text>());
                var combinedText = GetCombinedText(textElements);

                //order de keys van groot naar klein om conflicten met eve lange woorden te vermijden
                foreach (var pair in replacements.OrderBy(pair => pair.Key).Reverse())
                {
                    //PER PARAGRAAF VERGELIJK MET EEN REVERSED DICTONIARY //check of het template veld overeenkomt met de key
                    if (combinedText.Contains(pair.Key)) //INT ERN SHIP_VALUES //INTERNSHIP //values 
                    {
                        var enumerator = textElements.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var remainingKey = pair.Key;
                            var correctTextElements = new List<Text>();
                            var exists = true; //vermijd nulpointer error bij laatste moveNext()
                            var matchingChars = 0;

                            while (exists && (matchingChars = MatchingCharsAt(remainingKey, enumerator.Current.Text)) != 0)
                            {
                                remainingKey = remainingKey.Substring(matchingChars); //check op IINTERNSHIP 

                                correctTextElements.Add(enumerator.Current); //toevoegen van textElements 

                                if (remainingKey.Length == 0) //op eerste T element locatie
                                {
                                    //html en body tag toevoegen aan
                                    if (Regex.IsMatch(pair.Value, "<li>|<ol>|<p>|<a>|<i>|<b>"))
                                    {
                                        string temp = "";
                                        temp = pair.Value.Insert(0, @"<html><body style=""font-family:Verdana; font-size:14.5px"">");
                                        temp = temp.Insert(temp.Length, @"</body></html>");
                                        toReplace.Add(p, ChunkMethod(document, temp, ref chunkId));
                                    }
                                    else
                                    {
                                        correctTextElements[0].Text = pair.Value; //Kopieer data naar plaats textElement[0]
                                        for (var i = 1; i < correctTextElements.Count; i++)
                                        {
                                            correctTextElements[i].Text = ""; //volgende t elementen leeg laten -> later removen
                                        }
                                    }
                                }
                                exists = enumerator.MoveNext();
                            }
                        }
                    }
                }
            }
        }
        foreach (var entry in toReplace)
        {
            var index = openXmlElements.IndexOf(entry.Key);
            if (index != -1)
            {
                openXmlElements.RemoveAt(index);
                openXmlElements.Insert(index, entry.Value);
            }//altered allElements
        }
    }
    public void ExtractParagraphs(IEnumerable<OpenXmlElement> elements, string startSection, string endSection, out int startPosition, out List<OpenXmlElement> extratedParagraphs)
    {
        var position = 0;
        var inSection = false;
        extratedParagraphs = new List<OpenXmlElement>(); //
        startPosition = 0;

        foreach (var e in elements)
        {
            if (e is Paragraph)
            {
                var p = e as Paragraph;
                var textElements = e.Elements<Run>()
                    .SelectMany(r => r.Elements<Text>());

                var text = GetCombinedText(textElements);

                //Wanneer template paragraaf overeenkomt met meegegeven string, houdt positie bij 
                if (text.Contains(startSection))
                {
                    inSection = true;
                    startPosition = position;
                }

                if (text.Contains(endSection))
                {
                    inSection = false;
                }

                if (inSection)
                {
                    extratedParagraphs.Add(p);
                    if (p.Parent != null)
                    {
                        p.Remove();
                    }

                    //copy internships en delete achteraf van template 
                    //verwijder pas wanneer je inInternShip = false 
                    // en verwijder op positie paragraphPosition

                }
            }
            position++;
        }
    }
    public void InsertUnitParagraphs(WordprocessingDocument document, ref List<OpenXmlElement> allElements, List<UnitExportDto> unitList, List<OpenXmlElement> templateUnitParagraphs,
        int unitStartPosition, ref int chunkId, ResourceManager rm)
    {
        var reversedUnits = Reversed(unitList);

        for (var unitIndex = 0; unitIndex < reversedUnits.Count; unitIndex++) //Nieuwe lijst in omgekeerde volgorde 
        {
            var unit = reversedUnits[unitIndex];
            var unitElements = CloneELements(templateUnitParagraphs);

            Replace(document, ref unitElements, new Dictionary<string, string>()
                    {
                        {"UNIT_TITLE", unit.Name},
                        {"UNIT_DESCRIPTION", unit.PrefaceDto.Content},
                    }, ref chunkId);

            ExtractParagraphs(unitElements, "INTERNSHIP_TITLE", "INTERNSHIP_END", out var internshipStartPosition, out var templateInternshipParagraphs);

            unitElements.RemoveRange(internshipStartPosition, templateInternshipParagraphs.Count); //Delete niet ingevulde internship template  

            foreach (var internship in unit.InternShipsDtos)
            {
                var internshipParagraphs = CloneELements(templateInternshipParagraphs);

                Replace(document, ref internshipParagraphs, new Dictionary<string, string>()
                          {
                              {"INTERNSHIP_TITLE", internship.Translation.TitleContent},
                              {"INTERNSHIP_ASSIGNMENT_TITLE", rm.GetString("INTERNSHIP_ASSIGNMENT_TITLE")},
                              {"INTERNSHIP_ASSIGNMENT_DESCRIPTION", internship.Translation.Description},
                              {"INTERNSHIP_KNOWLEDGE_TITLE", rm.GetString("INTERNSHIP_KNOWLEDGE_TITLE")},
                              {"INTERNSHIP_KNOWLEDGE_DESCRIPTION", rm.GetString("INTERNSHIP_KNOWLEDGE_DESCRIPTION")},           //WHERE ARE YOU?? 
                              {"INTERNSHIP_KNOWLEDGE_CONTENT", internship.Translation.KnowledgeToDevelop},
                              {"INTERNSHIP_NEED_TO_KNOW_TITLE", rm.GetString("INTERNSHIP_NEED_TO_KNOW_TITLE")},
                              {"INTERNSHIP_NEED_TO_KNOW_DESCRIPTION", rm.GetString("INTERNSHIP_NEED_TO_KNOW_DESCRIPTION")},
                              {"INTERNSHIP_NEED_TO_KNOW_CONTENT", internship.Translation.NeededKnowledge},
                              {"INTERNSHIP_LOCATION_TITLE", rm.GetString("INTERNSHIP_LOCATION_TITLE")},
                              {"INTERNSHIP_LOCATION_CONTENT", MakingListOfHTMl(internship.Locations)},
                              {"INTERNSHIP_LOCATION_DESCRIPTION", rm.GetString("INTERNSHIP_LOCATION_DESCRIPTION")},
                              {"INTERNSHIP_LANGUAGE_TITLE", rm.GetString("INTERNSHIP_LANGUAGE_TITLE")},
                              {"INTERNSHIP_LANGUAGE_CONTENT", MakingListOfHTMl(internship.Languages)},
                              {"INTERNSHIP_COMMENTS_TITLE", rm.GetString("INTERNSHIP_COMMENTS_TITLE")},
                              {"INTERNSHIP_COMMENTS_CONTENT", rm.GetString("RequiredTrainingText")+ internship.TrainingType + internship.Translation.Comment},
                          }, ref chunkId);

                unitElements.InsertRange(internshipStartPosition, internshipParagraphs); //toevoegen van internshipParagraaf lijst in unit paragrafen 
            }

            //add new break
            if (unitIndex != 0)
            {
                unitElements.Add(new Break() { Type = BreakValues.Page });
            }

            //insert intrshipElements to allElements
            foreach (var element in Enumerable.Range(1, unitElements.Count())
                .Select(i => unitElements[unitElements.Count() - i])
                .ToList())
            {
                allElements.Insert(unitStartPosition, element);
            }
        }
    }

    public string MakingListOfHTMl<T>(IEnumerable<T> intrshpContent)
    {
        //string concatenatie 
        string locationString = "";
        foreach (var loc in intrshpContent)
        {
            string temp = "";
            temp = loc.ToString().Insert(0, "<li>");
            temp = temp.ToString().Insert(temp.Length, "</li>");
            locationString += temp;
        }
        locationString = locationString.Insert(0, "<ul>");
        locationString = locationString.Insert(locationString.Length, "</ul>");
        return locationString;
    }
    public int MatchingCharsAt(string s1, string s2)
    {
        var i = 0;
        while (i < s1.Length && i < s2.Length && s1[i] == s2[i])
        {
            i++;
        }
        return i;
    }
    public AltChunk ChunkMethod(WordprocessingDocument document, string html, ref int chunkId)
    {
        chunkId++;
        AltChunk altChunk = new AltChunk();

        //HTML
        AlternativeFormatImportPart chunk = document.MainDocumentPart!.AddAlternativeFormatImportPart(
            AlternativeFormatImportPartType.Html, $"altChunkId{chunkId}"); //this chunk will contain HTML

        using (Stream chunkStream = chunk.GetStream(FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter stringStream = new StreamWriter(chunkStream))
            {
                stringStream.Write(html);
            }

        }
        altChunk.Id = document.MainDocumentPart!.GetIdOfPart(chunk);
        return altChunk;
    }
    private void RemoveSdt(Body body)
    {
        var i = 0;
        foreach (var item in body.Elements())
        {
            if (item is SdtBlock)
            {
                body.RemoveChild(item);

                var sdtBlock = new SdtBlock();
                sdtBlock.InnerXml = GetTOC("Content", 16);
                body.InsertAt(sdtBlock, i);
                body.InsertAfter(new Break() { Type = BreakValues.Page }, sdtBlock);
            }
            i++;
        }
    }
    private string GetTOC(string title, int titleFontSize)
    {
        return $@"<w:sdt>
     <w:sdtPr>
        <w:id w:val=""-493258456"" />
        <w:docPartObj>
           <w:docPartGallery w:val=""Table of Contents"" />
           <w:docPartUnique />
        </w:docPartObj>
     </w:sdtPr>
     <w:sdtEndPr>
        <w:rPr>
           <w:rFonts w:asciiTheme=""minorHAnsi"" w:eastAsiaTheme=""minorHAnsi"" w:hAnsiTheme=""minorHAnsi"" w:cstheme=""minorBidi"" />
           <w:b />
           <w:bCs />
           <w:noProof />
           <w:color w:val=""auto"" />
           <w:sz w:val=""22"" />
           <w:szCs w:val=""22"" />
        </w:rPr>
     </w:sdtEndPr>
     <w:sdtContent>
        <w:p w:rsidR=""00095C65"" w:rsidRDefault=""00095C65"">
           <w:pPr>
              <w:pStyle w:val=""TOCHeading"" />
              <w:jc w:val=""center"" /> 
           </w:pPr>
           <w:r>
                <w:rPr>
                  <w:b /> 
                  <w:color w:val=""2E74B5"" w:themeColor=""accent1"" w:themeShade=""BF"" /> 
                  <w:sz w:val=""{titleFontSize * 2}"" /> 
                  <w:szCs w:val=""{titleFontSize * 2}"" /> 
              </w:rPr>
              <w:t>{title}</w:t>
           </w:r>
        </w:p>
        <w:p w:rsidR=""00095C65"" w:rsidRDefault=""00095C65"">
           <w:r>
              <w:rPr>
                 <w:b />
                 <w:bCs />
                 <w:noProof />
              </w:rPr>
              <w:fldChar w:fldCharType=""begin"" />
           </w:r>
           <w:r>
              <w:rPr>
                 <w:b />
                 <w:bCs />
                 <w:noProof />
              </w:rPr>
              <w:instrText xml:space=""preserve""> TOC \o ""1-3"" \h \z \u </w:instrText>
           </w:r>
           <w:r>
              <w:rPr>
                 <w:b />
                 <w:bCs />
                 <w:noProof />
              </w:rPr>
              <w:fldChar w:fldCharType=""separate"" />
           </w:r>
           <w:r>
              <w:rPr>
                 <w:noProof />
              </w:rPr>
              <w:t>No table of contents entries found.</w:t>
           </w:r>
           <w:r>
              <w:rPr>
                 <w:b />
                 <w:bCs />
                 <w:noProof />
              </w:rPr>
              <w:fldChar w:fldCharType=""end"" />
           </w:r>
        </w:p>
     </w:sdtContent>
  </w:sdt>";
    }
    public void DeleteParagraphs(ref List<OpenXmlElement> allElements, List<string> toDeleteParagraphs)
    {
        foreach (var e in allElements)
        {
            if (e is Paragraph)
            {
                var textElements = e.Elements<Run>()
                                       .SelectMany(r => r.Elements<Text>());

                var text = GetCombinedText(textElements);

                foreach (var item in toDeleteParagraphs)
                {
                    if (text.Contains(item))
                    {
                        e.RemoveAllChildren();
                    }
                }
            }
        }
    }
}



