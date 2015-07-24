using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot
{
    /// <summary>
    /// Represents an exception originating from Slack.
    /// </summary>
    public class SlackException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public SlackErrorCode ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public SlackException(SlackErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public SlackException(Exception innerException)
            : base(innerException.Message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        public SlackException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = (SlackErrorCode)info.GetValue("ErrorCode", typeof(SlackErrorCode));
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorCode", ErrorCode, typeof(SlackErrorCode));
        }
    }
}
