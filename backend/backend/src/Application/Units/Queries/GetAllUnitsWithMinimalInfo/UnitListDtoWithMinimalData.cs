using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Entities;

namespace backend.Application.Units.Queries.GetAllUnitsWithMinimalInfo;
public class UnitListDtoWithMinimalData:IMapFrom<Department>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, UnitListDtoWithMinimalData>();
    }
}
