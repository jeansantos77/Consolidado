using Consolidado.API.Application.Interfaces;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Interfaces;
using Consolidado.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consolidado.API.Application.Implementations
{
    public class LactoConsolidadoService : ILactoConsolidadoService
    {
        private readonly ILactoConsolidadoRepository _lactoConsolidadoRepository;

        public LactoConsolidadoService(ILactoConsolidadoRepository lactoConsolidadoRepository)
        {
            _lactoConsolidadoRepository = lactoConsolidadoRepository;
        }

        public async Task Add(LactoConsolidado entity)
        {
            await _lactoConsolidadoRepository.Add(entity);
        }

        public async Task<LactoConsolidadoModel> GetByDate(DateTime data)
        {
            return await _lactoConsolidadoRepository.GetByDate(data);
        }

        public async Task<List<LactoConsolidadoModel>> GetByRangeDate(DateTime startDate, DateTime endDate)
        {
            return await _lactoConsolidadoRepository.GetByRangeDate(startDate, endDate);
        }

        public async Task<LactoConsolidadoModel> GetLastBeforeDate(DateTime data)
        {
            return await _lactoConsolidadoRepository.GetLastBeforeDate(data);
        }

        public async Task ReprocessForward(DateTime data, decimal valor)
        {
            await _lactoConsolidadoRepository.ReprocessForward(data, valor);
        }

        public async Task Update(LactoConsolidado entity)
        {
            await _lactoConsolidadoRepository.Update(entity);
        }
    }
}
