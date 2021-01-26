using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Simulation.Service.Startup))]
namespace Simulation.Service
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Register HttpClientFactory
            builder.Services.AddHttpClient();
        }
    }
}
