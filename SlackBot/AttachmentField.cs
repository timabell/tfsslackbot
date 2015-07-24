using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents a realtime messaging attachment field.
    /// </summary>
    public sealed class AttachmentField
    {
        /// <summary>
        /// Gets the bold heading above the <see cref="Value"/>.
        /// </summary>
        /// <value>
        /// The bold heading above the <see cref="Value"/>.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the text value of the field.
        /// </summary>
        /// <value>
        /// The text value of the field.
        /// </value>
        public string Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="AttachmentField"/> is short and
        /// can be displayed next to other fields.
        /// </summary>
        /// <value>
        ///   <c>true</c> if short; otherwise, <c>false</c>.
        /// </value>
        public bool IsShort { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentField"/> class.
        /// </summary>
        /// <param name="title">The bold heading above the <paramref name="value"/>.</param>
        /// <param name="value">The text value of the field.</param>
        /// <param name="isShort">if set to <c>true</c> the field is short and can be displayed next to other fields.</param>
        public AttachmentField(string title, string value = null, bool isShort = false)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("title");

            Title = title;
            Value = value ?? string.Empty;
            IsShort = isShort;
        }

        /// <summary>
        /// Converts this <see cref="AttachmentField"/> into a dictionary suitable for JSON serialization.
        /// </summary>
        /// <returns>The dictionary.</returns>
        internal Dictionary<string, object> ToJson()
        {
            var o = new Dictionary<string, object>();
            o.Add("title", Title);
            o.Add("value", Value);
            if (IsShort) o.Add("short", true);
            return o;
        }
    }
}
