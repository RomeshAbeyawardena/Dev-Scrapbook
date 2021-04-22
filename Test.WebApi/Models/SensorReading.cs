using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi.Models
{
    public class SensorReading
    {
        public Guid DeviceId { get; set; }
        public string Type { get; set; }
        public string RawValue { get; set; }
        public DateTimeOffset TimestampUtc { get; set; }
        public decimal Value { 
            get 
            {
                if(decimal.TryParse(RawValue, out var decimalValue))
                {
                    return decimalValue;
                }
                else if (RawValue.Contains(":"))
                {
                    var splitEntries = RawValue.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    return decimal.Parse(splitEntries[0]);
                }

                return decimal.MinValue;
            } 
        }
    }
}
