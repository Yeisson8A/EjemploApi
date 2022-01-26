using Ejemplo4.Aplicacion.Cursos;
using Ejemplo4.Persistencia;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo4.Presentacion
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
            //Se agrega DbContext de proyecto Persistencia como servicio de proyecto WebAPI
            services.AddDbContext<CursosOnlineContext>(opt => {
                //Se usa cadena de conexión para conectarse a la base de datos
                //Se usa la variable Configuration que hace referencia al archivo appsettings.json,
                //y se obtiene la cadena de conexión
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //Se agregar Manejador del proyecto Aplicación como servicio de proyecto WebAPI
            services.AddMediatR(typeof(Consulta.Manejador).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EjemploApi", Version = "v1" });
                c.CustomSchemaIds(c => c.FullName);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EjemploApi v1"));
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
