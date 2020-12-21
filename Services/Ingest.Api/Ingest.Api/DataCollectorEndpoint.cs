using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ingest.Api
{
    public static class DataCollectorEndpoint
    {
        [FunctionName("DataCollectorEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "events/{eventType}")] HttpRequest req, string eventType, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var processorName = Environment.GetEnvironmentVariable("ProcessorName");
            return new OkObjectResult(processorName + " - " + eventType);
        }
    }
}
