using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ConsoleApp.Contracts;

namespace Test.ConsoleApp
{
    /// <inheritdoc cref="IRetryHandler"/>
    public class RetryHandler : IRetryHandler
    {
        private const int MinimumNumberOfTotalRetryAttempts = 0;
        private static readonly string Exception_MaximumNumberOfAttemptsBelowMinimum = "Total retry attempts should be at a minimum of " + MinimumNumberOfTotalRetryAttempts.ToString();
        private const string Exception_AttemptsExceeded = "{attemptMethodName} attempted {retryAttempts}/{totalRetryAttempts} times and failed";
        
        public TResult Attempt<TResult>(Func<TResult> attempt,
            Func<Exception, bool> retryExceptionCondition,
            int totalRetryAttempts,
            int retryInterval = 0,
            Action<int, Exception> failedAttempt = default)
        {
                return AttemptAsync(() => Task.FromResult(attempt()),
                    (exception) => Task.FromResult(retryExceptionCondition(exception)),
                    totalRetryAttempts,
                    retryInterval,
                    (attempts, exception) =>
                    {
                        failedAttempt(attempts, exception);
                        return Task.CompletedTask;
                    }).Result;
            
        }


        
        public TResult Attempt<TResult>(Func<TResult> attempt,
            int totalRetryAttempts, Action<int, Exception> failedAttempt = default,
            int retryInterval = 0, params Type[] exceptionTypes)
        {
            return AttemptAsync(() => Task.FromResult(attempt()),
                    totalRetryAttempts,
                    retryInterval,
                    (attempts, exception) =>
                    {
                        failedAttempt(attempts, exception);
                        return Task.CompletedTask;
                    }, exceptionTypes).Result;
        }

        
        public async Task<TResult> AttemptAsync<TResult>(Func<Task<TResult>> attempt,
            Func<Exception, Task<bool>> retryExceptionCondition,
            int totalRetryAttempts,
            int retryInterval = 0,
            Func<int, Exception, Task> failedAttempt = default)
        {
            if(attempt == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }

            if (retryExceptionCondition == null)
            {
                throw new ArgumentNullException(nameof(attempt));
            }

            if (totalRetryAttempts < MinimumNumberOfTotalRetryAttempts)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(attempt), 
                    Exception_MaximumNumberOfAttemptsBelowMinimum);
            }

            Exception lastException = null;
            var hasFailed = true;
            var retryAttempts = 0;
            while (hasFailed
                && retryAttempts < totalRetryAttempts)
            {
                try
                {
                    return await attempt();
                }
                catch (Exception exception)
                {
                    if (await retryExceptionCondition(exception))
                    {
                        lastException = exception;
                        retryAttempts++;
                        await failedAttempt (retryAttempts, exception);
                        await Task.Delay(retryInterval);
                        continue;
                    }

                    throw;
                }
            }
            var msg = Exception_AttemptsExceeded.ReplaceAll(
                KeyValuePair.Create("attemptMethodName", attempt.Method?.Name),
                KeyValuePair.Create("retryAttempts", retryAttempts.ToString()),
                KeyValuePair.Create("totalRetryAttempts", totalRetryAttempts.ToString()));
            throw new InvalidOperationException(msg, lastException);
        }

        
        public Task<TResult> AttemptAsync<TResult>(Func<Task<TResult>> attempt,
            int totalRetryAttempts, 
            int retryInterval = 0,
            Func<int, Exception, Task> failedAttempt = default,
            params Type[] exceptionTypes)
        {
            
            Task<bool> IsExceptionOfTypes(Exception exception)
            {
                var exceptionType = exception.GetType();

                return Task.FromResult(exceptionTypes.Any(t => exceptionType.BaseType == t 
                    || t == exceptionType));
            }

            return AttemptAsync(attempt, IsExceptionOfTypes, totalRetryAttempts, retryInterval, failedAttempt);
        }
    }
}
