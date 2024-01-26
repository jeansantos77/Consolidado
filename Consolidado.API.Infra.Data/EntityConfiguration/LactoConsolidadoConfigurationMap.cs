using Consolidado.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consolidado.API.Infra.Data.EntityConfiguration
{
    public class LactoConsolidadoConfigurationMap : IEntityTypeConfiguration<LactoConsolidado>
    {
        public void Configure(EntityTypeBuilder<LactoConsolidado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Creditos)
                   .IsRequired();
            
            builder.Property(c => c.Debitos)
                    .IsRequired();

            builder.Property(c => c.Saldo)
                    .IsRequired();

        }
    }
}
