using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents event information about an exception.
    /// </summary>
    public sealed class ExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ExceptionEventArgs(Exception exception)
        {
            if (exception == null)
            {
                try { throw new ArgumentNullException("exception"); }
                catch (Exception e) { exception = e; }
            }
            Exception = exception;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Exception"/> to <see cref="ExceptionEventArgs"/>.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ExceptionEventArgs(Exception e)
        {
            return new ExceptionEventArgs(e);
        }
    }
}
