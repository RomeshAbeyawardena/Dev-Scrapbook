using System.Collections.Generic;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the response object from Disruptive Tech API with a list of devices and their current status
    /// </summary>
    public class GetProjectDevicesResponse
    {
        public IEnumerable<Device> Devices { get; set; }
    }
}
