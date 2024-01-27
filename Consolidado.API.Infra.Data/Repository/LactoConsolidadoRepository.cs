using AutoMapper;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Interfaces;
using Consolidado.API.Domain.Models;
using Consolidado.API.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

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

        public void Add(LactoConsolidado entity)
        {
            _dbContext.Set<LactoConsolidado>().Add(entity);
            _dbContext.SaveChanges();
        }

        public LactoConsolidado GetByDate(DateTime data)
        {
            LactoConsolidado entity = _dbContext.Set<LactoConsolidado>().Where(x => x.Data.Date == data.Date).AsNoTracking().FirstOrDefault();
            return entity;
        }

        public List<LactoConsolidadoModel> GetByRangeDate(DateTime startDate, DateTime endDate)
        {
            List<LactoConsolidadoModel> models = _mapper.Map<List<LactoConsolidadoModel>>(_dbContext.Set<LactoConsolidado>()
                .Where(x => x.Data.Date >= startDate.Date && x.Data.Date <= endDate.Date)
                .OrderBy(x => x.Data)
                .AsNoTracking()
                .ToList());

            return models;
        }

        public LactoConsolidadoModel GetLastBeforeDate(DateTime data)
        {
            LactoConsolidado entidade = _dbContext.Set<LactoConsolidado>().Where(x => x.Data.Date < data.Date)
                .AsNoTracking()
                .OrderByDescending(x => x.Data)
                .FirstOrDefault();

            LactoConsolidadoModel model = _mapper.Map<LactoConsolidadoModel>(entidade);

            if (model == null)
            {
                model = new LactoConsolidadoModel
                {
                    Data = data,
                };
            }

            return model;
        }

        public void ReprocessForward(DateTime data, decimal saldoAnterior)
        {
            List<LactoConsolidado> lactos = _dbContext.Set<LactoConsolidado>().Where(x => x.Data.Date > data.Date)
                .OrderBy(x => x.Data)
                .ToList();

            foreach (var item in lactos)
            {
                item.Saldo = saldoAnterior + item.Creditos - item.Debitos;
                Update(item);
                saldoAnterior = item.Saldo;
            }
        }

        public void Update(LactoConsolidado entity)
        {
            _dbContext.Set<LactoConsolidado>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
