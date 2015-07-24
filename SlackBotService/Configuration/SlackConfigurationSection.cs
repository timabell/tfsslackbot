using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Configuration
{
    /// <summary>
    /// Represents the configuration section for Slack.
    /// </summary>
    public class SlackConfigurationSection : ConfigurationSection
    {
        [ThreadStatic]
        private static SlackConfigurationSection _current;
        /// <summary>
        /// Gets the current configuration section.
        /// </summary>
        /// <value>
        /// The current configuration section.
        /// </value>
        public static SlackConfigurationSection Current
        {
            get
            {
                if (_current == null) _current = (SlackConfigurationSection)ConfigurationManager.GetSection("slack");
                return _current;
            }
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        [ConfigurationProperty("client", IsRequired = true)]
        public ClientConfigurationElement Client
        {
            get { return (ClientConfigurationElement)base["client"]; }
        }

        /// <summary>
        /// Gets the sinks.
        /// </summary>
        /// <value>
        /// The sinks.
        /// </value>
        [ConfigurationProperty("sinks", IsRequired = true)]
        public SinkConfigurationElementCollection Sinks
        {
            get { return (SinkConfigurationElementCollection)base["sinks"]; }
        }
    }
}
