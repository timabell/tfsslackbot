using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Configuration
{
    /// <summary>
    /// Represents the client configuration element.
    /// </summary>
    public class ClientConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the bot token.
        /// </summary>
        /// <value>
        /// The bot token.
        /// </value>
        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get { return (string)base["token"]; }
        }
    }
}
