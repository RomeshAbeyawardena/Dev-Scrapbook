using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WebApi.Models;
using Models = Test.WebApi.Models;

namespace Test.WebApi.Features.Sensors
{
    public class Response
    {
        public Response(IEnumerable<Models.Sensor> sensors)
        {
            Sensors = sensors;
        }

        public IEnumerable<Sensor> Sensors { get; }
    }
}
