using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    sealed class RtmStartResponse
    {
        public object Ok { get; set; }
        public Uri Url { get; set; }
    }
}
