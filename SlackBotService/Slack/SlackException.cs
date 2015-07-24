using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    /// <summary>
    /// Represents an exception originating from Slack.
    /// </summary>
    public class SlackException : Exception
    {
        public SlackErrorCode ErrorCode
        {
            get;
            private set;
        }

        public SlackException(SlackErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public SlackException(Exception innerException)
            : base(innerException.Message, innerException)
        {

        }

        public static SlackException FromStatusCode(string status)
        {
            switch (status)
            {
                case "not_authed": return new SlackException(SlackErrorCode.NotAuthenticated);
                case "invalid_auth": return new SlackException(SlackErrorCode.InvalidAuthentication);
                case "not_in_channel": return new SlackException(SlackErrorCode.InvalidAuthentication);
                case "is_archived": return new SlackException(SlackErrorCode.ChannelIsArchived);
                case "msg_too_long": return new SlackException(SlackErrorCode.ChannelIsArchived);
                case "no_text": return new SlackException(SlackErrorCode.ChannelIsArchived);
                case "account_inactive": return new SlackException(SlackErrorCode.AccountInactive);
                case "rate_limited": return new SlackException(SlackErrorCode.RateLimited);
                default: return new SlackException(SlackErrorCode.Unknown);
            }
        }
    }
}
