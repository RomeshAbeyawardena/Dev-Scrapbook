using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device that is capable detecting touches
    /// </summary>
    public class TouchDeviceStatus : IDeviceStatus
    {
        public string Type { get; set; }
        public decimal? Reading { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
