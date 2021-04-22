using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp
{
    public class DeviceMessagePayload
    {
        /// <summary>
        /// Gets or sets the source of this reading (eg. 'sigfox' or 'rabbit')
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the sigfox device ID (this is the serial nunber we use against DeviceInstance)
        /// </summary>
        /// <value>The device serial number.</value>
        [JsonProperty("device")]
        public string DeviceSerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the timestamp (seconds since epoch?).
        /// </summary>
        /// <value>The timestamp.</value>
        [JsonProperty("time")]
        public long Timestamp { get; set; }

        [JsonIgnore]
        public DateTime? TimestampAsDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the data frame sent by the device.
        /// </summary>
        /// <value>The payload.</value>
        [JsonProperty("data")]
        public JObject Data { get; set; }

        /// <summary>
        /// Gets or sets the standard 'temperature' field for the Basic Temperature Device profile.
        /// </summary>
        /// <value>The temporary.</value>
        [JsonProperty("temp")]
        public long? Temp { get; set; }

        public DeviceMessagePayload()
        {

        }
    }
}
