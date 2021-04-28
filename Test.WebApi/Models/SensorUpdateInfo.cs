using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi.Models
{
    public class SensorUpdateInfo
    {
        public string Tags { get; set; }
        public Guid SensorId { get; set; }
        public DateTimeOffset TimestampUtc { get; set; }
    }
}
