using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nyous.Api.NOSQL.Contexts;
using Nyous.Api.NOSQL.Interfaces.Repositories;
using Nyous.Api.NOSQL.Repositories;

namespace Nyous.Api.NOSQL
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
            //Pega a informação do appsetting
            services.Configure<NyousDatabaseSettings>(
                Configuration.GetSection(nameof(NyousDatabaseSettings))
            );
            //Faz a injeção de dependecia
            services.AddSingleton<INyousDatabaseSettings>(
                sp => sp.GetRequiredService<IOptions<NyousDatabaseSettings>>().Value);

            services.AddSingleton<IEventoRepository, EventoRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
