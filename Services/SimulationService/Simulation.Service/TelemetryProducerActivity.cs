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

using Simulation.Service.Models;

namespace Simulation.Service
{
    public class TelemetryProducerActivity
    {
        private readonly HttpClient _client;


        public TelemetryProducerActivity(HttpClient httpClient)
        {
            _client = httpClient;
        }

        [FunctionName("TelemetryProducerActivity")]
        public async Task Run([TimerTrigger("*/10 * * * * *")]TimerInfo timer, ExecutionContext context, ILogger log)
        {
            log.LogInformation("In Timer Function");

            var config = context.BuildConfiguraion();

            var credential = new ClientSecretCredential(config["AuthTenantId"], config["AuthClientId"], config["AuthClientSecret"]);
            var accessToken = credential.GetToken(new TokenRequestContext(new[] { config["AuthScope"] }));


            var telemetry = RoomTelemetry.Generate();
            var payload = new StringContent(JsonConvert.SerializeObject(telemetry), Encoding.UTF8, "application/json");

            var url = config["EndpointUrl"];

            payload.Headers.Add("Ocp-Apim-Subscription-Key", config["Ocp-Apim-Subscription-Key"]);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            var response = await _client.PostAsync(url, payload);

            string result = response.Content.ReadAsStringAsync().Result;

            log.LogInformation($"Execution Result: {result}");
        }
    }
}
