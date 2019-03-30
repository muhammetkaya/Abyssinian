using Abyssinian.Messaging.Interaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Hosting.HostingStartup
{
    public abstract class StartupBase
    {
        private const string Url = "/swagger/v1/swagger.json";

        public static string Url1 => Url;

        public virtual void InitializeServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters();

            InitializeServices(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info: new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //}
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: Url1, name: "My API V1");
                c.RoutePrefix = "swagger/docs";
            });

            app.UseMvc();
        }
    }
}
