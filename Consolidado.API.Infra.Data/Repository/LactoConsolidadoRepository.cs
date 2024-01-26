using AutoMapper;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Interfaces;
using Consolidado.API.Domain.Models;
using Consolidado.API.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consolidado.API.Infra.Data.Repository
{
    public class LactoConsolidadoRepository : ILactoConsolidadoRepository
    {
        protected readonly ConsolidadoContext _dbContext;
        private readonly IMapper _mapper;

        public LactoConsolidadoRepository(ConsolidadoContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task Add(LactoConsolidado entity)
        {
            await _dbContext.Set<LactoConsolidado>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LactoConsolidadoModel> GetByDate(DateTime data)
        {
            LactoConsolidadoModel model = _mapper.Map<LactoConsolidadoModel>(await _dbContext.Set<LactoConsolidado>().Where(x => x.Data.Date == data).AsNoTracking().FirstOrDefaultAsync());
            return model;
        }

        public async Task<List<LactoConsolidadoModel>> GetByRangeDate(DateTime startDate, DateTime endDate)
        {
            List<LactoConsolidadoModel> models = _mapper.Map<List<LactoConsolidadoModel>>(await _dbContext.Set<LactoConsolidado>()
                .Where(x => x.Data.Date >= startDate && x.Data.Date <= endDate)
                .AsNoTracking()
                .ToListAsync());

            return models;
        }

        public async Task<LactoConsolidadoModel> GetLastBeforeDate(DateTime data)
        {
            LactoConsolidadoModel model = _mapper.Map<LactoConsolidadoModel>(await _dbContext.Set<LactoConsolidado>()
                .Where(x => x.Data.Date < data)
                .AsNoTracking()
                .OrderByDescending(x => x.Data)
                .FirstOrDefaultAsync());

            if (model == null)
            {
                model = new LactoConsolidadoModel
                {
                    Data = data,
                };
            }

            return model;
        }

        public Task ReprocessForward(DateTime data, decimal valor)
        {
            throw new NotImplementedException();
        }

        public async Task Update(LactoConsolidado entity)
        {
            _dbContext.Set<LactoConsolidado>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
