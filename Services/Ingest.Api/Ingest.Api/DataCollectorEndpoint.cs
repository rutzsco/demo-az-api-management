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
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "events/{customerName}")] HttpRequest req, string customerName, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            return new OkObjectResult(customerName);
        }
    }
}
