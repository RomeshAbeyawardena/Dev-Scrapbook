using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the response object from Disruptive Tech API for a single device and its historical status events
    /// </summary>
    public class GetProjectDeviceEventsResponse
    {
        public IEnumerable<DeviceEvent> Events { get; set; }
    }
}
