using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    /// <summary>
    /// Represents the client connection states.
    /// </summary>
    public enum ClientConnectionState
    {
        /// <summary>
        /// The client is disconnecting.
        /// </summary>
        Disconnecting = -1,
        /// <summary>
        /// The client is disconnected.
        /// </summary>
        Disconnected = 0,
        /// <summary>
        /// The client is connecting.
        /// </summary>
        Connecting = 1,
        /// <summary>
        /// The client is connected.
        /// </summary>
        Connected = 2,
        /// <summary>
        /// The connection has been completely established.
        /// </summary>
        Established = 3,
    }
}
