using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Commands.CreateInternShip;
using backend.Application.InternShips.Commands.UpdateInternShip;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.InternShips.Common;
public class ValidationFunctions
{
    readonly IApplicationDbContext _dbContext;

    public ValidationFunctions(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public static bool IsVersionValid(IList<TranslationUpdateInternshipDto> arg)
    {
        foreach (var version in arg)
        {
            if (String.IsNullOrEmpty(version.NeededKnowledge)) return false;
            if (String.IsNullOrEmpty(version.TitleContent)) return false;
            if (String.IsNullOrEmpty(version.Comment)) return false;
            if (String.IsNullOrEmpty(version.Description)) return false;
            if (String.IsNullOrEmpty(version.KnowledgeToDevelop)) return false;

        }
        return true;
    }
    public static bool IsVersionValid(IList<TranslationCreateInternshipDto> arg)
    {
        foreach (var version in arg)
        {
            if (String.IsNullOrEmpty(version.NeededKnowledge)) return false;
            if (String.IsNullOrEmpty(version.TitleContent)) return false;
            if (String.IsNullOrEmpty(version.Comment)) return false;
            if (String.IsNullOrEmpty(version.Description)) return false;
            if (String.IsNullOrEmpty(version.KnowledgeToDevelop)) return false;
        }
        return true;
    }
 
    public bool IsValidSchoolYear(string arg)
    {
        var currentYear = DateTime.UtcNow.Year;
        //var differenceWithMinimalYear = currentYear - 2020;
        for (int i = 0; i <= 20; i++)
        {
            var yearRange = $"{currentYear - i}-{currentYear - i +1}";
            if (yearRange == arg) return true;
        }

        return false;
    }
}
