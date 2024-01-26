using Consolidado.API.Domain.Entities;
using Consolidado.API.Infra.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Consolidado.API.Infra.Data.Context
{
    public class ConsolidadoContext : DbContext
    {
        public ConsolidadoContext(DbContextOptions<ConsolidadoContext> options) : base(options)
        {
            
        }

        public DbSet<LactoConsolidado> LactoConsolidados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LactoConsolidadoConfigurationMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
