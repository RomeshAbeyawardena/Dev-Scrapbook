using Newtonsoft.Json;
using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device that can report temperature
    /// </summary>
    public class TemperatureStatus : IDeviceStatus
    {
        [JsonProperty("value")]
        public decimal? Reading { get; set; }
        
        public DateTimeOffset UpdateTime { get; set; }

        public string Type { get; set; }
    }
}
