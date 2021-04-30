using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.WebApi.Models;

namespace Test.WebApi.Hubs
{
    public class SensorHub : Hub
    {
        private const string GetSensorUpdate = "GetSensorUpdate";
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendSensorUpdate(SensorUpdateInfo sensorUpdateInfo)
        {
            Console.WriteLine("{0}: {1} received:\r\nSensor Id:\t{2}\r\nTimestamp Utc:\t{3}\r\nTags:\t{4}", 
                DateTime.Now, 
                nameof(SensorHub),
                sensorUpdateInfo.SensorId, 
                sensorUpdateInfo.TimestampUtc,
                sensorUpdateInfo.Tags);
            //Get Sensor update notification
            await Clients.AllExcept(Context.ConnectionId)
                .SendAsync(GetSensorUpdate, sensorUpdateInfo);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
