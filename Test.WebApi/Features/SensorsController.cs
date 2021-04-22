using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApi.Base;
using GetSensorsFeature = Test.WebApi.Features.Sensors;
using GetSensorReadingsFeature = Test.WebApi.Features.SensorReadings;

namespace Test.WebApi.Features
{
    public class SensorsController : BaseController
    {
        [HttpGet] public Task<GetSensorsFeature.Response> GetSensors([FromQuery] GetSensorsFeature.Query query,
            CancellationToken cancellationToken)
        {
            return Mediator.Send(query, cancellationToken);
        }

        [HttpGet, Route("{sensorId}/{fromDate?}/{toDate?}")]
        public Task<GetSensorReadingsFeature.Response> GetSensorReadings([FromRoute] GetSensorReadingsFeature.Query query,
            CancellationToken cancellationToken)
        {
            return Mediator.Send(query, cancellationToken);
        }
    }
}
