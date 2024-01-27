using Consolidado.API.Application.Interfaces;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Interfaces;
using Consolidado.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Consolidado.API.Application.Implementations
{
    public class LactoConsolidadoService : ILactoConsolidadoService
    {
        private readonly ILactoConsolidadoRepository _lactoConsolidadoRepository;

        public LactoConsolidadoService(ILactoConsolidadoRepository lactoConsolidadoRepository)
        {
            _lactoConsolidadoRepository = lactoConsolidadoRepository;
        }

        public void Add(LactoConsolidado entity)
        {
            _lactoConsolidadoRepository.Add(entity);
        }

        public LactoConsolidado GetByDate(DateTime data)
        {
            return _lactoConsolidadoRepository.GetByDate(data);
        }

        public List<LactoConsolidadoModel> GetByRangeDate(DateTime startDate, DateTime endDate)
        {
            LactoConsolidadoModel beforeLacto = GetLastBeforeDate(startDate);

            List<LactoConsolidadoModel> lactos = _lactoConsolidadoRepository.GetByRangeDate(startDate, endDate);

            decimal saldoAnterior = beforeLacto.Saldo;  

            foreach (var item in lactos)
            {
                item.SaldoDiaAnterior = saldoAnterior;
                saldoAnterior = item.Saldo;
            }

            return lactos;

        }

        public LactoConsolidadoModel GetLastBeforeDate(DateTime data)
        {
            return _lactoConsolidadoRepository.GetLastBeforeDate(data);
        }

        public void ReprocessForward(DateTime data, decimal saldoAnterior)
        {
            _lactoConsolidadoRepository.ReprocessForward(data, saldoAnterior);
        }

        public void Update(LactoConsolidado entity)
        {
            _lactoConsolidadoRepository.Update(entity);
        }
    }
}
