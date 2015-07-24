using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Slack
{
    static class Json
    {

        /// <summary>
        /// Converts this <see cref="Attachment" /> into a dictionary suitable for JSON serialization.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns>
        /// The dictionary.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">attachment</exception>
        public static Dictionary<string, object> ToJson(this Attachment attachment)
        {
            if (attachment == null) throw new ArgumentNullException("attachment");

            var o = new Dictionary<string, object>();
            o.Add("fallback", attachment.Fallback);
            if (!string.IsNullOrEmpty(attachment.Color)) o.Add("color", attachment.Color);
            if (!string.IsNullOrEmpty(attachment.Pretext)) o.Add("pretext", attachment.Pretext);

            if (!string.IsNullOrEmpty(attachment.Author)) o.Add("author_name", attachment.Author);
            if (!string.IsNullOrEmpty(attachment.AuthorLink)) o.Add("author_link", attachment.AuthorLink);
            if (!string.IsNullOrEmpty(attachment.AuthorIcon)) o.Add("author_icon", attachment.AuthorIcon);

            if (!string.IsNullOrEmpty(attachment.Title)) o.Add("title", attachment.Title);
            if (!string.IsNullOrEmpty(attachment.TitleLink)) o.Add("title_link", attachment.TitleLink);

            if (!string.IsNullOrEmpty(attachment.Text)) o.Add("text", attachment.Text);

            if (attachment.Fields.Any())
                o.Add("fields", attachment.Fields.Select(x => x.ToJson()).ToList());

            if (!string.IsNullOrEmpty(attachment.ImageUrl)) o.Add("image_url", attachment.ImageUrl);
            if (!string.IsNullOrEmpty(attachment.ThumbUrl)) o.Add("thumb_url", attachment.ThumbUrl);

            return o;
        }

        /// <summary>
        /// Converts this <see cref="AttachmentField" /> into a dictionary suitable for JSON serialization.
        /// </summary>
        /// <param name="attachmentField">The attachment field.</param>
        /// <returns>
        /// The dictionary.
        /// </returns>
        public static Dictionary<string, object> ToJson(this AttachmentField attachmentField)
        {
            if (attachmentField == null) throw new ArgumentNullException("attachmentField");

            var o = new Dictionary<string, object>();
            o.Add("title", attachmentField.Title);
            o.Add("value", attachmentField.Value);
            if (attachmentField.IsShort) o.Add("short", true);
            return o;
        }

        /// <summary>
        /// Converts this <see cref="Message" /> into a dictionary suitable for JSON serialization.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// The dictionary.
        /// </returns>
        public static Dictionary<string, object> ToJson(this Message message)
        {
            if (message == null) throw new ArgumentNullException("message");

            var o = new Dictionary<string, object>();
            o.Add("channel", message.Channel);
            o.Add("type", "message");

            if (message.Text != null) o.Add("text", message.Text);
            if (message.User != null) o.Add("user", message.User);
            if (message.Subtype != MessageSubtypes.Message) o.Add("subtype", message.Subtype);
            if (message.Hidden) o.Add("hidden", message.Hidden);

            if (message.Attachments.Any())
                o.Add("attachments", message.Attachments.Select(x => x.ToJson()).ToList());

            return o;
        }

        /// <summary>
        /// Creates a <see cref="Message"/> from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The <see cref="Message"/>.</returns>
        public static Message Message(JsonObject obj)
        {
            var subtype = MessageSubtypes.Message;
            switch (obj["subtype"] ?? "")
            {
                case "bot_message": subtype = MessageSubtypes.BotMessage; break;
                case "me_message": subtype = MessageSubtypes.MeMessage; break;

                case "message_changed": subtype = MessageSubtypes.MessageChanged; break;
                case "message_deleted": subtype = MessageSubtypes.MessageDeleted; break;

                case "channel_join": subtype = MessageSubtypes.ChannelJoin; break;
                case "channel_leave": subtype = MessageSubtypes.ChannelLeave; break;
                case "channel_topic": subtype = MessageSubtypes.ChannelTopic; break;
                case "channel_purpose": subtype = MessageSubtypes.ChannelPurpose; break;
                case "channel_name": subtype = MessageSubtypes.ChannelName; break;
                case "channel_archive": subtype = MessageSubtypes.ChannelArchive; break;
                case "channel_unarchive": subtype = MessageSubtypes.ChannelUnarchive; break;

                case "group_join": subtype = MessageSubtypes.GroupJoin; break;
                case "group_leave": subtype = MessageSubtypes.GroupLeave; break;
                case "group_topic": subtype = MessageSubtypes.GroupTopic; break;
                case "group_purpose": subtype = MessageSubtypes.GroupPurpose; break;
                case "group_name": subtype = MessageSubtypes.GroupName; break;
                case "group_archive": subtype = MessageSubtypes.GroupArchive; break;
                case "group_unarchive": subtype = MessageSubtypes.GroupUnarchive; break;

                case "file_share": subtype = MessageSubtypes.FileShare; break;
                case "file_comment": subtype = MessageSubtypes.FileComment; break;
                case "file_mention": subtype = MessageSubtypes.FileMention; break;

                case "pinned_item": subtype = MessageSubtypes.PinnedItem; break;
                case "unpinned_item": subtype = MessageSubtypes.UnpinnedItem; break;
            }
            return new Message(
                    channel: obj["channel"],
                    text: obj["text"],
                    subtype: subtype,
                    user: obj["user"],
                    hidden: obj["hidden"] == "true");
        }

        /// <summary>
        /// Froms the status code.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static SlackException SlackException(string status)
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
