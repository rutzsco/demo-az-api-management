using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;

namespace Demo.Entitlements.Api
{
    public static class EntitlementsGetEndpoint
    {
        [FunctionName("EntitlementsGetEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var apiKey = req.Headers["Authorization"];
            log.LogInformation($"{apiKey}");

            return new OkObjectResult(new { status = 200 });
        }
    }
}
