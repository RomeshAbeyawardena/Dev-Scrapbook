using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WebApi.Models;

namespace Test.WebApi.Features.SensorReadings
{
    public class Response
    {
        public Response(IEnumerable<Models.SensorReading> sensorReadings)
        {
            SensorReadings = sensorReadings.OrderByDescending(a => a.TimestampUtc);
        }

        public IEnumerable<SensorReading> SensorReadings { get; }
    }
}
