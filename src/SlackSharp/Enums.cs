using System.Runtime.Serialization;

namespace SlackSharp
{
    public enum ResponseMessage
    {
        [EnumMember(Value = "-1")]
        None = -1,
        [EnumMember(Value = "0")]
        Ok = 0,
        [EnumMember(Value = "1")]
        NoTeam = 1,
        [EnumMember(Value = "2")]
        NoService = 2
    }
}
