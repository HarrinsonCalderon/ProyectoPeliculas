
using AutoMapper;
using back_end.FIltros;
using back_end.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end
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
            //configurar automaper
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(provider =>  
            new MapperConfiguration(config =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new autoMapperProfiles(geometryFactory));
            }).CreateMapper());
            services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

            //para las imagenes
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivos>();
            services.AddHttpContextAccessor();
            services.AddDbContext<AplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("defaultconnection"),
                sqlServer=>sqlServer.UseNetTopologySuite()));
            //configuracion de los cors
            services.AddCors(options => {
                //url configurada desde appseting.development.js para desarrollo
                var frontend_url = Configuration.GetValue<string>("frontend_url"); 
                options.AddDefaultPolicy(builder =>
                {
                    //ruta local angular
                    //builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                    builder.WithOrigins(frontend_url).AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
                });
            });
            
            //para el filtro de autorizacion accion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            //para los controller
           

            //filtro de exception, solo con esto se ejecuta en toda la aplicacion
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(filtroDeException));
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "back_end", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "back_end v1"));
            }

            app.UseHttpsRedirection();
            //para las imagenes
            app.UseStaticFiles();
            app.UseRouting();
            //para los cors
            app.UseCors();

            //middlegare de autenticacion
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
