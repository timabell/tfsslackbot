using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Tfs.Configuration
{
    /// <summary>
    /// Represents the TFS configuration section.
    /// </summary>
    public class TfsConfigurationSection : ConfigurationSection
    {
        [ThreadStatic]
        private static TfsConfigurationSection _current;
        /// <summary>
        /// Gets the current configuration section.
        /// </summary>
        /// <value>
        /// The current configuration section.
        /// </value>
        public static TfsConfigurationSection Current
        {
            get
            {
                if (_current == null) _current = (TfsConfigurationSection)ConfigurationManager.GetSection("slackTfs");
                return _current;
            }
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
