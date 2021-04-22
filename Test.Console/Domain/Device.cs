using Newtonsoft.Json;

namespace Test.ConsoleApp.Domain
{
    /// <summary>
    /// Represents a physical device that may contain <see cref="DeviceData"/>
    /// </summary>
    public class Device
    {
        private string _name;
        public string Id  { get; private set; }

        public string Name { 
            get { return _name; } 
            set 
            { 
                _name = value; 
                Id = GetId(); 
            } 
        }

        public string Type { get; set; }
        public DeviceLabel Labels { get; set; }

        [JsonProperty("reported")]
        public DeviceData Data { get; set; }

        private string GetId()
        {
            var splitName = _name.Split('/', System.StringSplitOptions.RemoveEmptyEntries);

            if(splitName.Length > 1)
            {
                return splitName[splitName.Length -1]?.Trim();
            }

            return _name;
        }   
    }
}
