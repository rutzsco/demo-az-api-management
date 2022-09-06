using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Service.Logic
{
    public class GenericGetExecutionLogic
    {
        private string _url;
        private string _accessToken;
        private HttpClient _httpClient;
        private ILogger _logger;

        public GenericGetExecutionLogic(string url, string accessToken, HttpClient httpClient, ILogger logger)
        {
            _url = url ?? throw new ArgumentNullException(nameof(url));
            _accessToken = accessToken;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute(string api)
        {
            if (!string.IsNullOrEmpty(_accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await _httpClient.GetAsync(_url+"/"+ api);
            string result = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"API call result - Api:{api} Status: {response.StatusCode}");
         }
    }
}
