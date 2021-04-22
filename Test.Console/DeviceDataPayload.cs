using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Domain
{
    public class DeviceDataPayload
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public decimal? Reading { get; set; }
    }
}
