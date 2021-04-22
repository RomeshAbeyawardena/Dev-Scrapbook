using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp
{
    public class Program
    {
        static int attempts;
        public static async Task Main()
        {
            var retryHandler = new RetryHandler();

            Console.WriteLine(
                await retryHandler.AttemptAsync(async () => {
                    var dependency = DependencyProviderMethodThatWillFailAsync();
                    return await DependentMethod(await dependency);
                }, 5, 1000, 
                async (failedAttempts, exception) => { 
                    attempts = failedAttempts; 
                    await Task.CompletedTask; 
                    Console.Error.WriteLine("Attempted: {0} times\r\nException: {1}\r\nInner Exception (Optional): {2}\r\n\r\n", failedAttempts, exception.Message, exception.InnerException?.Message); 
                }, typeof(ArgumentException)));
        }

        static string FailedMethod()
        {
            if (attempts == 20)
            {
                return "Hello";
            }
            else
                throw new ArgumentException($"Expected attempts to be 4 not {attempts}");
        }

        static async Task<string> DependentMethod(string value)
        {
            await Task.CompletedTask;
            return $"{nameof(DependentMethod)} recieved '{value}'";
        }

        static async Task<string> DependencyProviderMethodThatWillFailAsync()
        {
            if (attempts == 4)
            {
                return "Hello";
            }
            else if(attempts < 2)
            {
                throw new ArgumentNullException("Expected parameter was null (not really)");
            }
            else
                throw new ArgumentException($"Expected attempts to be 4 not {attempts}");
        }
    }
}
