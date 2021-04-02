using Newtonsoft.Json;

using Simulation.Service.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Service.Logic
{
    public class GenericPostExecutionLogic
    {
        private string _url;
        private string _accessToken;
        private HttpClient _httpClient;

        public GenericPostExecutionLogic(string url, string accessToken, HttpClient httpClient)
        {
            _url = url ?? throw new ArgumentNullException(nameof(url));
            _accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task Execute()
        {

            var telemetry = RoomTelemetry.Generate();
            var payload = new StringContent(JsonConvert.SerializeObject(telemetry), Encoding.UTF8, "application/json");

            //payload.Headers.Add("Ocp-Apim-Subscription-Key", config["Ocp-Apim-Subscription-Key"]);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await _httpClient.PostAsync(_url, payload);
            string result = await response.Content.ReadAsStringAsync();
        }
    }
}
