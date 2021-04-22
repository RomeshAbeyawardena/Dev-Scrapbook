using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test.ConsoleApp
{
    public static class StringExtensions
    {
		public static string ReplaceAll(this string value, 
			params KeyValuePair<string, string>[] replacementKeyValues)
        {
			var newValue = value;
            foreach (var keyValue in replacementKeyValues)
            {
				newValue = newValue.Replace(keyValue.Key, keyValue.Value);
            }

			return newValue;
        }

		/// <summary>
		/// Parses a string replacing commands with replacement values supplied in contentDictionary/>
		/// </summary>
		/// <param name="input"></param>
		/// <param name="contentDictionary"></param>
		/// <param name="commandStartCharacter"></param>
		/// <param name="commandEndCharacter"></param>
		/// <param name="commandParameterCharacter"></param>
		/// <returns></returns>
		public static string ParseString(
			this string input,
			Dictionary<string, string> contentDictionary,
			char commandStartCharacter,
			char commandEndCharacter,
			char commandParameterCharacter)
		{
			var newInput = input;
			//foreach (var keyValuePair in contentDictionary)
			//{
			//	newInput = newInput.Replace("[" + keyValuePair.Key.ToLower() + "]", keyValuePair.Value);
			//}

			var commands = input.ParseCommands(
				commandStartCharacter,
				commandEndCharacter,
				commandParameterCharacter);

			foreach (var command in commands)
			{
				if (command.IsParameterised)
				{
					var targets = command.Target.Split(',', StringSplitOptions.RemoveEmptyEntries);

					var items = contentDictionary.Where(s => targets.Contains(s.Key));

					if (command.Name == "optional")
					{
						newInput = newInput.Replace(command.Expression, items.Any(item => string.IsNullOrWhiteSpace(item.Value))
							? string.Empty
							: command.Parameter);
					}
				}
                else
                {
					if(contentDictionary.TryGetValue(command.Name, out var item))
                    {
						newInput = newInput.Replace(command.Expression, string.IsNullOrWhiteSpace(item)
							? command.Parameter
							: item);
					}
                }
			}

			return newInput.TrimEnd();
		}

		/// <summary>
		/// Retrieves a list of commands from a string based on specified characters
		/// </summary>
		/// <param name="input"></param>
		/// <param name="commandStartCharacter"></param>
		/// <param name="commandEndCharacter"></param>
		/// <param name="commandParameterCharacter"></param>
		/// <returns></returns>
		public static IEnumerable<Command> ParseCommands(
			this string input,
			char commandStartCharacter,
			char commandEndCharacter,
			char commandParameterCharacter)
		{
			var commandList = new List<Command>();

			bool inCommandRegion = false;
			bool isCommandName = false;
			bool isCommandParameter = false;
			string commandName = string.Empty;
			string commandParameter = string.Empty;
			string expression = string.Empty;
			foreach (char c in input)
			{
				if (c == commandStartCharacter)
				{
					expression = c.ToString();
					commandName = string.Empty;
					inCommandRegion = true;
					isCommandName = true;
					continue;
				}

				if (inCommandRegion)
				{
					expression += c;
					if (c == commandParameterCharacter)
					{
						if (isCommandParameter)
						{
							commandParameter += c;
						}
						else
						{
							isCommandName = false;
							isCommandParameter = true;
							commandParameter = string.Empty;
						}
						continue;
					}

					if (c == commandEndCharacter)
					{
						inCommandRegion = false;
						var command = new Command
						{
							NameWithTarget = commandName,
							Parameter = commandParameter,
							Expression = expression
						};

						command.SetCommandAndTarget();
						commandList.Add(command);
						commandParameter = string.Empty;
						expression = string.Empty;
						continue;
					}

					if (isCommandName)
					{
						commandName += c;
						continue;
					}

					if (isCommandParameter)
					{
						commandParameter += c;
						continue;
					}

				}


			}

			return commandList.ToArray();
		}

		public static string ConvertEmailsToHyperlinks(
			this string inputValue)
		{
			var emailAddressElements = new[] { '.', '@' };
			var words = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			var newInput = string.Empty;
			foreach(var word in words)
            {
				if (emailAddressElements
					.All(element => word.Contains(element)))
                {
					newInput += string.Format("<a href=\"mailto:{0}\">{0}</a> ",word);
					continue;
				}

				newInput += word + " ";
			}

			return newInput;
		}
	}
}
