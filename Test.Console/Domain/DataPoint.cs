using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Domain
{
    public class DataPoint
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public decimal? Reading { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
