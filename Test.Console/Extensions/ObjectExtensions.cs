using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Extensions
{
    public static class ObjectExtensions
    {
		public static T DeserializeJson<T>(this string response,
			JsonSerializer jsonSerializer = default)
		{
			if (jsonSerializer == null)
			{
				jsonSerializer = JsonSerializer.CreateDefault();
			}

			using (var textReader = new StringReader(response))
			{
				using (var jsonReader = new JsonTextReader(textReader))
					return jsonSerializer
						.Deserialize<T>(jsonReader);
			}
		}

		public static IEnumerable<TInterface> GetInstancesBasedonPropertyTypes<TInterface>(this object value, 
			Action<PropertyInfo, TInterface> applyAdditionalLogic = default)
		{
			var deviceEventType = value.GetType();

			var deviceStatusProperties = deviceEventType
				.GetProperties()
				.Where(t => t.PropertyType
					.GetInterfaces()
					.Any(a => (a.IsInterface || a.IsAbstract)
						&& a == typeof(TInterface)));

			return deviceStatusProperties
				.Select(GetInstanceBasedonProperty);

			TInterface GetInstanceBasedonProperty(PropertyInfo property)
            {
				var implmentedValue = (TInterface)property.GetValue(value);
				applyAdditionalLogic?.Invoke(property, implmentedValue);
				return implmentedValue;
			}
		}
	}
}
