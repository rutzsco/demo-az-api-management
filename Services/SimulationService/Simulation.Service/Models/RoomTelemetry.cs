using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation.Service.Models
{
    public class RoomTelemetry
    {
        public RoomTelemetry(string assetId, double temperature, double humidity)
        {
            Id = Guid.NewGuid().ToString();
            RoomId = assetId ?? throw new ArgumentNullException(nameof(assetId));
            Temperature = temperature;
            Humidity = humidity;
        }

        public string Id { get; set; }

        public string RoomId { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public static RoomTelemetry Generate()
        {
            Random rnd = new Random();
            var roomId = "ROOM" + String.Format("{0:000}", rnd.Next(1, 11));
            var temperature = Math.Round(GetRandomNumberInRange(60, 80),1);
            var humidity = Math.Round(GetRandomNumberInRange(10, 60),1);

            if (roomId.Equals("ROOM001") || roomId.Equals("ROOM010"))
            {
                temperature = 85;
            }
            return new RoomTelemetry(roomId, temperature, humidity);
        }

        public static double GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }
    }
}
