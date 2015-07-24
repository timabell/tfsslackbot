using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents different states that chat message processing can result in.
    /// </summary>
    public enum ChatMessageSinkResult
    {
        /// <summary>
        /// Continue processing with other chat message sinks.
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Don't execute any more message sinks.
        /// </summary>
        Complete = 1
    }
}
