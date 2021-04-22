using Newtonsoft.Json;
using System;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents the status of a device that can be toggled between two states
    /// </summary>
    public class StateDeviceStatus : IDeviceStatus
    {
        private const string State_Present = "PRESENT";
        private const string State_NotPresent = "NOT_PRESENT";
        private string state;

        public string Type { get; set; }

        public decimal? Reading { get; set; }
        
        public string State { 
            get { return state; } 
            set { 
                state = value;
                if (state == State_Present)
                {
                    Reading = 1m;
                }
                else if(state == State_NotPresent)
                {
                    Reading = 0m;
                }
            } 
        }

        public DateTimeOffset UpdateTime { get; set; }
    }
}
