using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    /// <summary>
    /// Represents the slack error codes.
    /// </summary>
    public enum SlackErrorCode
    {
        /// <summary>
        /// The error is not defined by this API.
        /// </summary>
        Unknown = 0x0,
        /// <summary>
        /// The authentication is required to make the request.
        /// </summary>
        NotAuthenticated = 0x1,
        /// <summary>
        /// The authentication provided is invalid.
        /// </summary>
        InvalidAuthentication = 0x2,
        /// <summary>
        /// The integration is not in the channel.
        /// </summary>
        NotInChannel = 0x3,
        /// <summary>
        /// The channel has been archived.
        /// </summary>
        ChannelIsArchived = 0x4,
        /// <summary>
        /// The message is too long.
        /// </summary>
        MessageTooLong = 0x5,
        /// <summary>
        /// No message text was provided.
        /// </summary>
        NoText = 0x6,
        /// <summary>
        /// The authentication token represents an integration that was deleted.
        /// </summary>
        AccountInactive = 0x7,
        
        /// <summary>
        /// A flag indicating transient errors.
        /// </summary>
        Transient = 0x1000,
        /// <summary>
        /// The application has posted too many messages.
        /// </summary>
        RateLimited = Transient | 0x1,
    }
}
