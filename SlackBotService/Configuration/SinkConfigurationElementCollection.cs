using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace SlackBot.Configuration
{
    /// <summary>
    /// Represent a collection of <see cref="SinkConfigurationElement"/> objects.
    /// </summary>
    public class SinkConfigurationElementCollection : ConfigurationElementCollection, IEnumerable<SinkConfigurationElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinkConfigurationElementCollection"/> class.
        /// </summary>
        public SinkConfigurationElementCollection()
        {

        }

        /// <summary>
        /// Creates the sinks.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="System.Threading.CancellationToken.None"/>.</param>
        /// <returns>
        /// A list of <see cref="Task{IChatMessageSink}" /> objects that can be awaited to retrieve each sink.
        /// </returns>
        public IEnumerable<Task<IChatMessageSink>> CreateSinks(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var elem in this)
            {
                var tcs = new TaskCompletionSource<IChatMessageSink>();

                try
                {
                    var sink = (IChatMessageSink)Activator.CreateInstance(Type.GetType(elem.Type, true), null);
                    var awaiter = sink.InitializeAsync(elem.Name, cancellationToken).ConfigureAwait(true).GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        awaiter.GetResult();
                        tcs.SetResult(sink);
                    }
                    else
                    {
                        awaiter.OnCompleted(() =>
                        {
                            try
                            {
                                awaiter.GetResult();
                                tcs.SetResult(sink);
                            }
                            catch (Exception e)
                            {
                                tcs.SetException(e);
                            }
                        });
                    }
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }

                yield return tcs.Task;
            }
        }

        /// <summary>
        /// Creates a new <see cref="SinkConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="SinkConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SinkConfigurationElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="SinkConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="SinkConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SinkConfigurationElement)element).Name;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public new IEnumerator<SinkConfigurationElement> GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this).OfType<SinkConfigurationElement>().GetEnumerator();
        }
    }
}
