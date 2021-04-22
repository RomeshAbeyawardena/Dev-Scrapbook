using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Contracts
{
    /// <summary>
    /// Represents a retry handler used reattempt a method after a given number of attempts
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IRetryHandler
    {
        /// <summary>
        /// <para>Invokes a <see cref="Delegate"/> for a specified <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/>, 
        /// if a <paramref name="retryInterval"/> is provided the task will attempt the <see cref="Delegate"/> again after the specified period elapses</para>
        /// <para>Each failure invokes the <paramref name="failedAttempt"/> <see cref="Delegate"/></para>
        /// <para>After the total <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/> are exceeded the last <see cref="Exception"/> will be thrown </para>
        /// </summary>
        /// <typeparam name="TResult">The result of <paramref name="attempt"/></typeparam>
        /// <param name="attempt"></param>
        /// <param name="retryExceptionCondition"></param>
        /// <param name="totalRetryAttempts"></param>
        /// <param name="retryInterval"></param>
        /// <param name="failedAttempt"></param>
        /// <returns>The <typeparamref name="TResult"/> of <paramref name="attempt"/></returns>
        /// <exception cref="InvalidOperationException"></exception>
        TResult Attempt<TResult>(Func<TResult> attempt,
            Func<Exception, bool> retryExceptionCondition,
            int totalRetryAttempts,
            int retryInterval = 0,
            Action<int, Exception> failedAttempt = default);

        /// <summary>
        /// <para>Invokes a <see cref="Delegate"/> for a specified <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/>, 
        /// if a <paramref name="retryInterval"/> is provided the task will attempt the <see cref="Delegate"/> again after the specified period elapses</para>
        /// <para>Each failure invokes the async <paramref name="failedAttempt"/> <see cref="Delegate"/> providing the current <see cref="Int32">number</see> of attempts</para>
        /// <para>After the total <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/> are exceeded the last <see cref="Exception"/> will be thrown </para>
        /// </summary>
        /// <typeparam name="TResult">The result of <paramref name="attempt"/></typeparam>
        /// <param name="attempt"></param>
        /// <param name="totalRetryAttempts"></param>
        /// <param name="failedAttempt"></param>
        /// <param name="exceptionTypes"></param>
        /// <returns>The <typeparamref name="TResult"/> of <paramref name="attempt"/></returns>
        /// <exception cref="InvalidOperationException"></exception>
        TResult Attempt<TResult>(Func<TResult> attempt,
            int totalRetryAttempts, Action<int, Exception> failedAttempt = default,
            int retryInterval = 0, params Type[] exceptionTypes);

        /// <summary>
        /// <para>Invokes a <see cref="Delegate"/> for a specified <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/>, 
        /// if a <paramref name="retryInterval"/> is provided the task will attempt the <see cref="Delegate"/> again after the specified period elapses</para>
        /// <para>Each failure invokes the async <paramref name="failedAttempt"/> <see cref="Delegate"/> providing the current <see cref="Int32">number</see> of attempts</para>
        /// <para>After the total <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/> are exceeded the last <see cref="Exception"/> will be thrown </para>
        /// </summary>
        /// <typeparam name="TResult">The result of <paramref name="attempt"/></typeparam>
        /// <param name="attempt"></param>
        /// <param name="retryExceptionCondition"></param>
        /// <param name="totalRetryAttempts"></param>
        /// <param name="retryInterval"></param>
        /// <param name="failedAttempt"></param>
        /// <returns>The <typeparamref name="TResult"/> of <paramref name="attempt"/></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<TResult> AttemptAsync<TResult>(Func<Task<TResult>> attempt,
            Func<Exception, Task<bool>> retryExceptionCondition,
            int totalRetryAttempts,
            int retryInterval = 0,
            Func<int, Exception, Task> failedAttempt = default);

        /// <summary>
        /// <para>Invokes a <see cref="Delegate"/> for a specified <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/>, 
        /// if a <paramref name="retryInterval"/> is provided the task will attempt the <see cref="Delegate"/> again after the specified period elapses</para>
        /// <para>Each failure invokes the async <paramref name="failedAttempt"/> <see cref="Delegate"/> providing the current <see cref="Int32">number</see> of attempts</para>
        /// <para>After the total <see cref="Int32">number</see> of <paramref name="totalRetryAttempts"/> are exceeded the last <see cref="Exception"/> will be thrown </para>
        /// </summary>
        /// <typeparam name="TResult">The result of <paramref name="attempt"/></typeparam>
        /// <param name="attempt"></param>
        /// <param name="totalRetryAttempts"></param>
        /// <param name="failedAttempt"></param>
        /// <param name="exceptionTypes"></param>
        /// <returns>The <typeparamref name="TResult"/> of <paramref name="attempt"/></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<TResult> AttemptAsync<TResult>(Func<Task<TResult>> attempt,
            int totalRetryAttempts,
            int retryInterval = 0,
            Func<int, Exception, Task> failedAttempt = default,
            params Type[] exceptionTypes);
    }
}
