using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents a message attachment.
    /// </summary>
    public sealed class Attachment
    {
        /// <summary>
        /// Gets the plain-text summary of the attachment.
        /// </summary>
        /// <value>
        /// The plain-text summary of the attachment.
        /// </value>
        public string Fallback { get; private set; }

        /// <summary>
        /// Gets the color of the border along the left side of the message attachment.
        /// </summary>
        /// <value>
        /// The color of the border along the left side of the message attachment.
        /// </value>
        public string Color { get; private set; }

        /// <summary>
        /// Gets the optional message that appears above the attachment block.
        /// </summary>
        /// <value>
        /// The optional message that appears above the attachment block.
        /// </value>
        public string Pretext { get; private set; }

        /// <summary>
        /// Gets the author's name.
        /// </summary>
        /// <value>
        /// The author's name.
        /// </value>
        public string Author { get; private set; }

        /// <summary>
        /// Gets the link to the author's website.
        /// </summary>
        /// <value>
        /// The link to the author's website.
        /// </value>
        public string AuthorLink { get; private set; }

        /// <summary>
        /// Gets the link to the author's icon.
        /// </summary>
        /// <value>
        /// The link to the author's icon.
        /// </value>
        public string AuthorIcon { get; private set; }

        /// <summary>
        /// Gets the larger, bold text near the top of a message attachment.
        /// </summary>
        /// <value>
        /// The larger, bold text near the top of a message attachment.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the link location for <see cref="Title"/>.
        /// </summary>
        /// <value>
        /// The link location for <see cref="Title"/>.
        /// </value>
        public string TitleLink { get; private set; }

        /// <summary>
        /// Gets the main text in a message attachment.
        /// </summary>
        /// <value>
        /// The main text in a message attachment.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the image file that will be displayed inside a message attachment.
        /// </summary>
        /// <value>
        /// The image file that will be displayed inside a message attachment.
        /// </value>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// Gets the image file that will be displayed as a thumbnail on the right side of a message attachment.
        /// </summary>
        /// <value>
        /// The image file that will be displayed as a thumbnail on the right side of a message attachment.
        /// </value>
        public string ThumbUrl { get; private set; }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public IReadOnlyList<AttachmentField> Fields { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment" /> class.
        /// </summary>
        /// <param name="fallback">The plain-text summary of the attachment.</param>
        /// <param name="color">The color of the border along the left side of the message attachment.</param>
        /// <param name="pretext">The optional text that appears above the message attachment block.</param>
        /// <param name="author">The author's name.</param>
        /// <param name="authorLink">The link to the author's website.</param>
        /// <param name="authorIcon">The author's icon.</param>
        /// <param name="title">The larger, bold text near the top of a message attachment.</param>
        /// <param name="titleLink">The link location for <paramref name="title"/>.</param>
        /// <param name="text">The main text in a message attachment.</param>
        /// <param name="imageUrl">The image file that will be displayed inside a message attachment.</param>
        /// <param name="thumbUrl">The image file that will be displayed as a thumbnail on the right side of a message attachment.</param>
        /// <param name="fields">The fields.</param>
        /// <exception cref="System.ArgumentNullException">fallback</exception>
        public Attachment(
            string fallback, string color = null, string pretext = null,
            string author = null, string authorLink = null, string authorIcon = null,
            string title = null, string titleLink = null,
            string text = null,
            string imageUrl = null, string thumbUrl = null,
            IEnumerable<AttachmentField> fields = null)
        {
            if (string.IsNullOrEmpty(fallback)) throw new ArgumentNullException("fallback");

            Fallback = fallback;
            Color = color ?? string.Empty;
            Pretext = pretext ?? string.Empty;
            Author = author ?? string.Empty;
            AuthorLink = authorLink ?? string.Empty;
            AuthorIcon = authorIcon ?? string.Empty;
            Title = title ?? string.Empty;
            TitleLink = titleLink ?? string.Empty;
            Text = text ?? string.Empty;
            ImageUrl = imageUrl ?? string.Empty;
            ThumbUrl = thumbUrl ?? string.Empty;

            Fields = new ReadOnlyCollection<AttachmentField>((fields ?? Enumerable.Empty<AttachmentField>()).ToList());
        }
    }
}
