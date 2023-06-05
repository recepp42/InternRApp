using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Domain.Entities;

namespace backend.Application.InternShips.Queries.GetInternShipById;
public class DepartmentMinimalDataDto : IMapFrom<Department>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, DepartmentMinimalDataDto>();
    }
}
