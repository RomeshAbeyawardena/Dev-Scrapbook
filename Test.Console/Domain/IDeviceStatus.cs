using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device that can report an updated timestamp
    /// </summary>
    public interface IDeviceStatus
    {
        string Type { get; set; }
        /// <summary>
        /// The reading of the device
        /// </summary>
        decimal? Reading { get; set; }
        /// <summary>
        /// Last time the device status was updated
        /// </summary>
        DateTimeOffset UpdateTime { get; set; }
    }
}
