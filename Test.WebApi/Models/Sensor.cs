using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi.Models
{
    public class Sensor
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string SerialNumber { get; set; }
        public string Tags { get; set; }
        public string SensorTypes { get; set; }

        public IEnumerable<string> Types => SensorTypes.Split(',');
    }
}
