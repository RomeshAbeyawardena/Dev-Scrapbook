using JWT;
using JWT.Algorithms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Test.ConsoleApp.Domain;
using Test.ConsoleApp.Extensions;

namespace Test.ConsoleApp
{
	public class Program2
	{
		public const string OptionalCommand = "optional";
		public const char CommandStartCharacter = '[';
		public const char CommandEndCharacter = ']';
		public const char CommandParameterCharacter = ':';
		public const string ProviderName = "DisruptiveTech";
		public const string DisruptiveTechTag = "Disruptive-Tech";
		private static JsonSerializer jsonSerializer;
		public static async Task Main2()
		{
			var jwtEncoder = new JWT.JwtEncoder(new HMACSHA256Algorithm(),
				new JWT.Serializers.JsonNetSerializer(), 
				new JwtBase64UrlEncoder());

			var apiClient = new DisruptiveTechAPIClient(
				"https://api.d21s.com/v2",
				"c1q28ff915gg00f3tsag@bve8hsgacd1000c0dr30.serviceaccount.d21s.com",
				"c1qp1r6761f000biknag", 
				"2c29cc68e53846078e7db92d4cfc1686",
				"bve8hsgacd1000c0dr30", jwtEncoder);
			jsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings { Formatting = Formatting.Indented });

			var accessToken = apiClient.Login();
			
			apiClient.UseLoginApiAcccessToken(await accessToken);
			
			var projectDevices = apiClient.GetProjectDevices();

			foreach(var projectDevice in (await projectDevices).Devices)
            {
				Console.WriteLine("Id: {0}\r\nName: {1}\r\nType: {2}\r\nLabel {3}\r\n\r\n",
					projectDevice.Id,
					projectDevice.Name,
					projectDevice.Type,
					projectDevice.Labels.Name);

				var dateTime = new DateTime(2021, 04, 13, 09, 12, 0);
				var response = await apiClient.GetProjectDeviceEvents(
					projectDevice.Id, 
					dateTime, 
					dateTime.AddHours(24),
					10);

				var eventDictionary = new Dictionary<DateTimeOffset, IEnumerable<DeviceDataPayload>>();
				var dataPoints = new List<DataPoint>();

				void SetType(PropertyInfo property, IDeviceStatus deviceStatus)
				{
					if (deviceStatus != null)
					{
						var displayNameAttribute = property
							.GetCustomAttribute<DisplayNameAttribute>();
						deviceStatus.Type = displayNameAttribute?.DisplayName ?? property.Name;
					}
				}

				foreach (var deviceEvent in response.Events)
				{
					//Flatten device statuses using reflection, get the type of device status
					//from a display name attribute or the actual property name
					var deviceStatuses = deviceEvent.Data
						.GetInstancesBasedonPropertyTypes<IDeviceStatus>(SetType)
						.Where(a => a != null && a.Reading.HasValue);

					dataPoints.AddRange(deviceStatuses
							.Select(a => new DataPoint
							{
								DeviceId = projectDevice.Id,
								DeviceType = a.Type,
								Reading = a.Reading,
								TimeStamp = a.UpdateTime.UtcDateTime
							}));
				}

				var timeGroups = dataPoints
					   .GroupBy(p => p.TimeStamp.Date
						   .AddHours(p.TimeStamp.Hour)
						   .AddMinutes(p.TimeStamp.Minute)
						   .AddSeconds((p.TimeStamp.Second / 5) * 5)) // round to the nearest whole 5 seconds (.second is an int!)
					   .ToList();

				var messagesToQueue = new List<DeviceMessagePayload>();
				foreach (var g in timeGroups)
				{
					try
					{
						// g is our reading group
						var payload = new JObject();

						// Take the most recent reading of all mentioned sensors we've captured in this slice
						// (eg. if we get two flow_temp readings, take the most recent only)
						foreach (var reading in g.GroupBy(i => i.DeviceId).Select(sg => sg.Last()))
						{
							payload.Add(new JProperty(reading.DeviceType, reading.Reading));
						}

						var message = new DeviceMessagePayload
						{
							TimestampAsDateUtc = g.Max(i => i.TimeStamp),
							DeviceSerialNumber = "f9342u9j3",
							Source = DisruptiveTechTag,
							Data = payload
						};

						messagesToQueue.Add(message);
					}
					catch (Exception ex)
					{
						Console.Error.WriteLine($"Failed to parse {ProviderName} datapoint JSON for device {"ews"}: {ex.Message}");
					}

					Console.WriteLine(Serialize(messagesToQueue));
				}
			}
			//var dateTime = new DateTime(2021, 04, 13, 09, 12, 0);
			//var response = apiClient.GetProjectDeviceEvents(
			//	"bjej50vbluqg00dltt9g", 
			//	dateTime, 
			//	dateTime.AddHours(24));

			//Console.WriteLine(Serialize(response));

			Console.ReadKey();
		}

		

		public static string Serialize<T>(T model)
        {
			using (var textWriter = new StringWriter())
			{
				using (var jsonTextWriter = new JsonTextWriter(textWriter))
					jsonSerializer.Serialize(jsonTextWriter, model);

				return textWriter.ToString();
			}
		}
	}

	public class Command
	{
		public string Name { get; private set; }
		public string Target { get; private set; }
		public string Expression { get; set; }
		public string NameWithTarget { get; set; }
		public string Parameter { get; set; }
		public bool IsParameterised { get; set; }
		public void SetCommandAndTarget()
        {
			var matches = Regex.Match(NameWithTarget, "([A-z]+)[(]([,A-z]+)[)]");
			IsParameterised = matches.Success;

			if (matches.Success)
            {
				Name = matches.Groups[1].Value;
				Target = matches.Groups[2]?.Value;
            }
            else
            {
				Name = NameWithTarget;
            }
        }
	}
}
