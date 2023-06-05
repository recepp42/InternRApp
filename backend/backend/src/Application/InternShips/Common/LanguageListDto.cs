using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Domain.Entities;

namespace backend.Application.InternShips.Common;
public class LanguageListDto:IMapFrom<Language>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code{ get; set; }
    public int? CreatorId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Language, LanguageListDto>();
    }
    public override string ToString()
    {
        return Name;
    }
}
