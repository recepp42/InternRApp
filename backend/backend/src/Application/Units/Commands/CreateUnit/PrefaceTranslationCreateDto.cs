using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Application.Units.Commands.UpdateUnit;
using backend.Domain.Entities;

namespace backend.Application.Units.Commands.CreateUnit;
public class PrefaceTranslationCreateDto 
{
    public string Content { get; set; }
    public int LanguageId { get; set; }

   
}
