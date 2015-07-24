using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents an realtime message subtype.
    /// </summary>
    [Flags]
    public enum MessageSubtypes
    {
        /// <summary>
        /// Indicates that a human is the originator of a message.
        /// </summary>
        Human = 0x1000,
        /// <summary>
        /// All <see cref="Human"/>-type messages.
        /// </summary>
        AllHuman = Human | 0xFFF,
        /// <summary>
        /// A normal message was sent.
        /// </summary>
        Message = Human | 0x0,
        /// <summary>
        /// A /me message was sent
        /// </summary>
        MeMessage = Human | 0x1,

        /// <summary>
        /// Indicates that an integration is the originator of a message.
        /// </summary>
        Integration = 0x2000,
        /// <summary>
        /// All <see cref="Integration"/>-type messages.
        /// </summary>
        AllIntegration = Integration | 0xFFF,
        /// <summary>
        /// A message was posted by an integration.
        /// </summary>
        BotMessage = Integration | 0x0,

        /// <summary>
        /// Indicates that an edit is the originator of a message.
        /// </summary>
        Editing = 0x4000,
        /// <summary>
        /// All <see cref="Editing"/>-type messages.
        /// </summary>
        AllEditing = Editing | 0xFFF,
        /// <summary>
        /// A message was changed.
        /// </summary>
        MessageChanged = Editing | 0x0,
        /// <summary>
        /// A message was deleted.
        /// </summary>
        MessageDeleted = Editing | 0x1,

        /// <summary>
        /// Indicates that a channel change is the originator of a message.
        /// </summary>
        Channel = 0x8000,
        /// <summary>
        /// All <see cref="Channel"/>-type messages.
        /// </summary>
        AllChannel = Channel | 0xFFF,
        /// <summary>
        /// A team member joined a channel.
        /// </summary>
        ChannelJoin = Channel | 0x0,
        /// <summary>
        /// A team member left a channel.
        /// </summary>
        ChannelLeave = Channel | 0x1,
        /// <summary>
        /// A channel topic was updated.
        /// </summary>
        ChannelTopic = Channel | 0x2,
        /// <summary>
        /// A channel purpose was updated.
        /// </summary>
        ChannelPurpose = Channel | 0x4,
        /// <summary>
        /// A channel was renamed.
        /// </summary>
        ChannelName = Channel | 0x8,
        /// <summary>
        /// A channel was archived.
        /// </summary>
        ChannelArchive = Channel | 0x10,
        /// <summary>
        /// A channel was unarchived.
        /// </summary>
        ChannelUnarchive = Channel | 0x20,

        /// <summary>
        /// Indicates that a group change is the originator of a message.
        /// </summary>
        Group = 0x10000,
        /// <summary>
        /// All <see cref="Group"/>-type messages.
        /// </summary>
        AllGroup = Group | 0xFFF,
        /// <summary>
        /// A team member joined a group.
        /// </summary>
        GroupJoin = Group | 0x0,
        /// <summary>
        /// A team member left a group.
        /// </summary>
        GroupLeave = Group | 0x1,
        /// <summary>
        /// A group topic was updated.
        /// </summary>
        GroupTopic = Group | 0x2,
        /// <summary>
        /// A group purpose was updated.
        /// </summary>
        GroupPurpose = Group | 0x4,
        /// <summary>
        /// A group was renamed.
        /// </summary>
        GroupName = Group | 0x8,
        /// <summary>
        /// A group was archived.
        /// </summary>
        GroupArchive = Group | 0x10,
        /// <summary>
        /// A group was unarchived.
        /// </summary>
        GroupUnarchive = Group | 0x20,

        /// <summary>
        /// Indicates that a file is the originator of a message.
        /// </summary>
        File = 0x20000,
        /// <summary>
        /// All <see cref="File"/>-type messages.
        /// </summary>
        AllFile = File | 0xFFF,
        /// <summary>
        /// A file was shared into a channel.
        /// </summary>
        FileShare = File | 0x0,
        /// <summary>
        /// A comment was added to a file.
        /// </summary>
        FileComment = File | 0x1,
        /// <summary>
        /// A file was mentioned in a channel.
        /// </summary>
        FileMention = File | 0x2,

        /// <summary>
        /// Indicates that a pin is the originator of a message.
        /// </summary>
        Pin = 0x40000,
        /// <summary>
        /// All <see cref="Pin"/>-type messages.
        /// </summary>
        AllPin = Pin | 0xFFF,
        /// <summary>
        /// An item was pinned in a channel.
        /// </summary>
        PinnedItem = Pin | 0x0,
        /// <summary>
        /// An item was unpinned in a channel.
        /// </summary>
        UnpinnedItem = Pin | 0x1
    }
}
