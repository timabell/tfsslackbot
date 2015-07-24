using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents event arguments about a realtime message.
    /// </summary>
    public sealed class Message : EventArgs
    {
        /// <summary>
        /// Gets the channel that the message was sent to.
        /// </summary>
        /// <value>
        /// The channel that the message was sent to.
        /// </value>
        public string Channel { get; private set; }

        /// <summary>
        /// Gets the user that sent the message.
        /// </summary>
        /// <value>
        /// The user that sent the message.
        /// </value>
        public string User { get; private set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the message subtype.
        /// </summary>
        /// <value>
        /// The message subtype.
        /// </value>
        public MessageSubtypes Subtype { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Message"/> is hidden.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hidden; otherwise, <c>false</c>.
        /// </value>
        public bool Hidden { get; private set; }

        /// <summary>
        /// Gets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public IReadOnlyList<Attachment> Attachments { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="text">The text.</param>
        /// <param name="subtype">The message subtype.</param>
        /// <param name="user">The user.</param>
        /// <param name="hidden">if set to <c>true</c> the message is hidden.</param>
        /// <exception cref="System.ArgumentNullException">channel</exception>
        public Message(string channel, string text, MessageSubtypes subtype = MessageSubtypes.Message, string user = null, bool hidden = false, IEnumerable<Attachment> attachments = null)
        {
            if (string.IsNullOrEmpty(channel)) throw new ArgumentNullException("channel");

            Channel = channel;
            Text = text ?? string.Empty;
            User = user;
            Subtype = subtype;
            Hidden = hidden;
            Attachments = new ReadOnlyCollection<Attachment>((attachments ?? Enumerable.Empty<Attachment>()).ToList());
        }

        /// <summary>
        /// Creates a reply message.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="attachments">The attachments.</param>
        /// <returns>The reply message.</returns>
        public Message CreateReply(string text, IEnumerable<Attachment> attachments = null)
        {
            return new Message(Channel, text, attachments: attachments);
        }
    }
}
