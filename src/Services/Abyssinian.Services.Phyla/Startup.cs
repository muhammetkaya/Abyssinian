using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Abyssinian.Hosting.HostingStartup;

namespace Abyssinian.Services.Phyla
{
    public class Startup: Hosting.HostingStartup.StartupBase
    {
        public override void InitializeServices(IServiceCollection services)
        {

        }
    }
}
