using Consolidado.API.Application.Implementations;
using Consolidado.API.Application.Interfaces;
using Consolidado.API.Domain.Interfaces;
using Consolidado.API.Infra.Data.AutoMapper;
using Consolidado.API.Infra.Data.Context;
using Consolidado.API.Infra.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Consolidado.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Consolidado.API", Version = "v1" });
            });

            services.AddDbContext<ConsolidadoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(); // Ativar rastreamento detalhado
            } /*, ServiceLifetime.Transient*/);

            services.AddAutoMapper(typeof(AutoMappings));
            services.AddScoped<ILactoConsolidadoRepository, LactoConsolidadoRepository>();
            services.AddScoped<ILactoConsolidadoService, LactoConsolidadoService>();

            services.AddHostedService<RabbitMQWorkerService>();

            services.Configure<QueueConfig>(Configuration.GetSection("RabbitMQ"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consolidado.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
