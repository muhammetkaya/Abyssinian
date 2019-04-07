using Abyssinian.Hosting.Extensions;
using Abyssinian.Hosting.Settings;
using Abyssinian.Messaging.Interaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abyssinian.Hosting.HostingStartup
{
    public abstract class StartupBase
    {
        private readonly IConfiguration _configuration;

        private const string _url = "/swagger/v1/swagger.json";
        public string Url
        {
            get
            {
                return _url;
            }
        }

        public InterServiceCommunicationSettings _settings { get; set; }
        public InterServiceCommunicationSettings Settings
        {
            get
            {
                return _settings;
            }
        }

        protected StartupBase()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            _configuration = new ConfigurationBuilder()
                .AddJsonFile($"{currentDirectory}\\appSettings.json")
                .Build();

            _configuration.GetSection("InterServiceCommunication")
                .Bind(_settings = new InterServiceCommunicationSettings());
        }

        public virtual void InitializeCustomServices(IServiceCollection services)
        { }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters();

            services.UseInterServiceCommunication(Settings);

            InitializeCustomServices(services);
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
                c.SwaggerEndpoint(url: Url, name: "My API V1");
                c.RoutePrefix = "swagger/docs";
            });

            app.UseMvc();
        }
    }
}
