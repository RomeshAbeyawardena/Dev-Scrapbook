using Newtonsoft.Json;
using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device that can report its own wireless signal strength
    /// </summary>    
    public class NetworkStatus : IDeviceStatus
    {
        public int SignalStrength { get; set; }

        [JsonProperty("rssi")]
        public decimal? Reading { get; set; }

        public DateTimeOffset UpdateTime { get; set; }
        public string TransmissionMode { get; set; }

        public string Type { get; set; }
    }
}
