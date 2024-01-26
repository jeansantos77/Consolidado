using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Consolidado.API.Infra.Data.Context
{
    public class ConsolidadoContextFactory : IDesignTimeDbContextFactory<ConsolidadoContext>
    {
        public ConsolidadoContext CreateDbContext(string[] args)
        {
            return CreateDbContext("DefaultConnection");
        }
        public ConsolidadoContext CreateDbContext(string connectionStringName)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ConsolidadoContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionStringName));

            return new ConsolidadoContext(optionsBuilder.Options);
        }
    }
}
