using AutoMapper;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Models;
using System.Collections.Generic;

namespace Consolidado.API.Infra.Data.AutoMapper
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<LactoConsolidadoModel, LactoConsolidado>()
                .ReverseMap();
            CreateMap<List<LactoConsolidadoModel>, List<LactoConsolidado>>()
                .ReverseMap();
        }
    }
}
