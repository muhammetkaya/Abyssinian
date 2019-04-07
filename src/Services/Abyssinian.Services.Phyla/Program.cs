using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Abyssinian.Services.Phyla
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var _configuration = new ConfigurationBuilder()
                .AddJsonFile($"{currentDirectory}\\appSettings.json")
                .Build();

            string _url = _configuration.GetValue<string>("Url");
            int _port = _configuration.GetValue<Int32>("Port");

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls($"{_url}:{_port}")
                .UseStartup<Startup>();
        }
    }
}
