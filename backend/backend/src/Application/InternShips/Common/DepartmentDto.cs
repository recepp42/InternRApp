using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using backend.Application.Common.Mappings;
using backend.Domain.Entities;

namespace backend.Application.InternShips.Common;
public class DepartmentDto:IMapFrom<Department>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<string> ManagerEmails { get; set; }

    public List<InternShip> InternShips { get; set; }
    public void Mapping(Profile profile)
    {
       profile.CreateMap<Department, DepartmentDto>();
    }

    public bool Equals(DepartmentDto x, DepartmentDto y)
    {
        if (x == null || y == null)
        {
            return false;
        }
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] DepartmentDto obj)
    {
        return obj.Id.GetHashCode();
    }
}
