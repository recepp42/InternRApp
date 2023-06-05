using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Application.InternShips.Queries.GetAllInternShips;
using backend.Application.Units.Common;
using backend.Domain.Entities;
using backend.Domain.Enums;


namespace backend.Application.InternShips.Queries.GetExportInternShipData;
public class UnitExportDto //unitListDTO met bijbehorende internships  
{
    //public int DepartmentId { get; set; }   
    //public DepartmentDto Unit { get; set; } 
    //public List<int> InternShipId { get; set; } 

    public string Name { get; set; }
    public PrefaceTranslationDto PrefaceDto { get; set; }
    //public List<InternShipListDto> InternShips { get; set; }
    public List<InternShipExportDto> InternShipsDtos { get; set; }


}
