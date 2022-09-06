using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Azure.Core;
using Azure.Identity;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Simulation.Service.Logic;
using Simulation.Service.Models;

namespace Simulation.Service
{
    public class ApiCallProducerActivity
    {
        private readonly HttpClient _client;


        public ApiCallProducerActivity(HttpClient httpClient)
        {
            _client = httpClient;
        }

        [FunctionName("ApiCallProducerActivity")]
        public async Task Run([TimerTrigger("*/10 * * * * *")]TimerInfo timer, ExecutionContext context, ILogger log)
        {
            log.LogInformation("In Timer Function");

            string accessToken = null;
            var config = context.BuildConfiguraion();
            if (!string.IsNullOrEmpty(config["AuthTenantId"]))
                accessToken = config.GetAccessToken();

            var url = config["EndpointUrl"];
            var key = config["SubscriptionKey"];
            var logic = new GenericGetExecutionLogic(url, accessToken, key, _client, log);

            await logic.Execute("topics");
            await logic.Execute("speakers");
        }
    }
}
