using System.Runtime.Serialization;

namespace SlackSharp
{
    /// <summary>
    /// The results of sending messages with Incoming WebHooks.
    /// </summary>
    public enum ResponseMessage
    {
        /// <summary>Undefined.</summary>
        [EnumMember(Value = "-1")]
        None = -1,
        /// <summary>ok.</summary>
        [EnumMember(Value = "0")]
        Ok = 0,
        /// <summary>noteam.</summary>
        [EnumMember(Value = "1")]
        NoTeam = 1,
        /// <summary>noservice.</summary>
        [EnumMember(Value = "2")]
        NoService = 2
    }
}
