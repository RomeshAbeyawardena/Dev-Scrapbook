using Newtonsoft.Json;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents a device label attached to the device in the DT dashboard settings
    /// </summary>
    public class DeviceLabel
    {
        public string Name { get; set; }
        public string Kit { get; set; }

        [JsonProperty("dt-purchase-order")]
        public string PurchaseOrder { get; set; }
    }
}
