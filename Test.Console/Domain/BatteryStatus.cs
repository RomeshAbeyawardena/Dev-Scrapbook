using Newtonsoft.Json;
using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device that is capable of reading its own battery status
    /// </summary>
    public class BatteryStatus : IDeviceStatus
    {
        [JsonProperty("percentage")]
        public decimal? Reading { get; set; }
        
        public DateTimeOffset UpdateTime { get; set; }

        public string Type { get; set; }
    }
}
