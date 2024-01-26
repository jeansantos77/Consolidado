using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consolidado.API.Domain.Interfaces
{
    public interface ILactoConsolidadoRepository
    {
        Task Add(LactoConsolidado entity);
        Task Update(LactoConsolidado entity);
        Task<LactoConsolidadoModel> GetByDate(DateTime data);
        Task<List<LactoConsolidadoModel>> GetByRangeDate(DateTime startDate, DateTime endDate);
        Task<LactoConsolidadoModel> GetLastBeforeDate(DateTime data);
        Task ReprocessForward(DateTime data, decimal valor);
    }
}
