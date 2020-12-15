using System;
using System.Threading.Tasks;

using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

using Simulation.Service.Models;

namespace Simulation.Service
{
    public class TelemetryProducerActivity
    {
        [FunctionName("TelemetryProducerActivity")]
        public async Task Run([TimerTrigger("*/30 * * * * *")]TimerInfo timer, ILogger log)
        {
            log.LogInformation("In Timer Function");

            var telemetry = RoomTelemetry.Generate();
            var myEvent = new EventGridEvent(telemetry.Id, telemetry.RoomId, telemetry, "TrapTelemetry", DateTime.UtcNow, "1.0");

        }
    }
}
