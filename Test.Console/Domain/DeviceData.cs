using Newtonsoft.Json;
using System.ComponentModel;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device with multiple status check facilities
    /// </summary>
    public class DeviceData
    {
        public bool HasNetworkStatusData => Network != null;

        public bool HasBatteryStatusData => Battery != null;
        
        public bool HasTemperatureStatusData => Temperature != null;

        public bool HasStateDeviceStatusData => State != null;

        public bool HasTouchStatusData => Touch != null;

        [JsonProperty("networkStatus"),
         DisplayName("net_rssi")]
        public NetworkStatus Network { get; set; }

        [JsonProperty("batteryStatus"), 
         DisplayName("batteryVoltage")]
        public BatteryStatus Battery { get; set; }

        [DisplayName("temp")]
        public TemperatureStatus Temperature { get; set; }

        [JsonProperty("objectPresent"),
         DisplayName("pressure")]
        public StateDeviceStatus State { get; set; }

        [DisplayName("touch_state")]
        public TouchDeviceStatus Touch { get; set; }
    }
}
