using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Models;
using System;
using System.Collections.Generic;

namespace Consolidado.API.Domain.Interfaces
{
    public interface ILactoConsolidadoRepository
    {
        void Add(LactoConsolidado entity);
        void Update(LactoConsolidado entity);
        LactoConsolidado GetByDate(DateTime data);
        List<LactoConsolidadoModel> GetByRangeDate(DateTime startDate, DateTime endDate);
        LactoConsolidadoModel GetLastBeforeDate(DateTime data);
        void ReprocessForward(DateTime data, decimal valor);
    }
}
