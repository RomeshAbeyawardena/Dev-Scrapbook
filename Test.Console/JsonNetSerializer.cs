using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;
using System;
using Test.ConsoleApp.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test.ConsoleApp
{
    public class JsonNetSerializer : IRestSerializer
    {
        private const string jsonContentType = "application/json";
        private readonly JsonSerializer jsonSerializer;

        public JsonNetSerializer()
            : this(null)
        {

        }

        public JsonNetSerializer(JsonSerializer jsonSerializer)
        {
            if(jsonSerializer == null)
            {
                jsonSerializer = JsonSerializer.CreateDefault();
            }

            SupportedContentTypes = new[] { jsonContentType };
            DataFormat = DataFormat.Json;
            
            this.jsonSerializer = jsonSerializer;
        }

        public string[] SupportedContentTypes { get; }

        public DataFormat DataFormat { get; }

        public string ContentType { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            return response.Content
                .DeserializeJson<T>(jsonSerializer);
        }

        public string Serialize(Parameter parameter)
        {
            return Serialize(parameter as object);
        }

        public string Serialize(object value)
        {
            using (var textWriter = new StringWriter())
            {
                using (var jsonWriter = new JsonTextWriter(textWriter))
                    jsonSerializer.Serialize(jsonWriter, value);

                return textWriter.ToString();
            }
        }
    }
}
