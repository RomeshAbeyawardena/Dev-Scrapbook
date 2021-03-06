using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.WebApi.Hubs;

namespace Test.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ApplicationSettings>()
                .AddTransient<IDbConnection>(a => new SqlConnection(GetApplicationSettings(a).DefaultConnectionString))
                .AddMediatR(typeof(Startup).Assembly)
                .AddControllers();

            services
                .AddCors()
                .AddSignalR();
        } 

        public static ApplicationSettings GetApplicationSettings(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ApplicationSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(poliy => poliy
                .WithOrigins(
                    "http://localhost:5200", 
                    "https://localhost:5201")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapHub<SensorHub>("/sensorhub");
                endpoints.MapControllers();
            });
        }
    }
}
