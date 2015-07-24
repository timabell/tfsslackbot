using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// I'm allowed to, but not you.
    /// </summary>
    public interface ISlack
    {
        /// <summary>
        /// Asynchronously sends the specified message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="System.Threading.CancellationToken.None"/>.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous send operation.
        /// </returns>
        Task SendAsync(Message message, CancellationToken cancellationToken = default(CancellationToken));
    }
}
