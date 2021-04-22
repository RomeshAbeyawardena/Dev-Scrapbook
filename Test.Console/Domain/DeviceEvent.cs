using Newtonsoft.Json;
using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents a historic record for a specfic device that may contain <see cref="DeviceData"/>
    /// </summary>
    public class DeviceEvent
    {
        [JsonProperty("eventId")]
        public string Id { get; set;  }

        [JsonProperty("targetName")]
        public string Name { get; set; }

        [JsonProperty("eventType")]
        public string Type { get; set; }

        public DeviceData Data { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}
