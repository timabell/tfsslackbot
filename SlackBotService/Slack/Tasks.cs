using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    /// <summary>
    /// Extension methods for <see cref="Task"/>.
    /// </summary>
    static class Tasks
    {
        /// <summary>
        /// Converts the specified task to APM style.
        /// </summary>
        /// <typeparam name="T">The result type of the task.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns>The <see cref="IAsyncResult"/> to return from the method.</returns>
        /// <exception cref="System.ArgumentNullException">task</exception>
        public static IAsyncResult AsApm<T>(this Task<T> task, AsyncCallback callback, object state)
        {
            if (task == null) throw new ArgumentNullException("task");
            var tcs = new TaskCompletionSource<T>(state);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted) tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled) tcs.TrySetCanceled();
                else tcs.TrySetResult(t.Result);

                if (callback != null) callback(tcs.Task);
            }, TaskScheduler.Default);
            return tcs.Task;
        }
    }
}
