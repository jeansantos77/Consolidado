using AutoMapper;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Consolidado.API.Infra.Data.AutoMapper
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<LactoConsolidado, LactoConsolidadoModel>()
                .ForMember(dest => dest.SaldoDiaAnterior, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<LactoConsolidadoModel, LactoConsolidado>()
                .ReverseMap();

            CreateMap<QueueMessageModel, LactoConsolidadoModel>()
                .ForMember(dest => dest.Saldo, opt => opt.Ignore())
                .ReverseMap();
            
            CreateMap<List<LactoConsolidadoModel>, List<LactoConsolidado>>()
                .ConvertUsing((src, dest, context) =>
                {
                    var result = src.Select(item => context.Mapper.Map<LactoConsolidado>(item)).ToList();
                    return result;
                });

            CreateMap<List<LactoConsolidado>, List<LactoConsolidadoModel>>()
                .ConvertUsing((src, dest, context) =>
                {
                    var result = src.Select(item => context.Mapper.Map<LactoConsolidadoModel>(item)).ToList();
                    return result;
                });


        }
    }
}
