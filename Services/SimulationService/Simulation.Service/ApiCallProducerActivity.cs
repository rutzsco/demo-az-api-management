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

            var config = context.BuildConfiguraion();

            var url = config["EndpointUrl"];
            var logic = new GenericGetExecutionLogic(url, "", _client);

            logic.Execute("sessions");
            logic.Execute("speakers");
        }
    }
}
