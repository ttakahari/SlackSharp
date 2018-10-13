using System.Runtime.Serialization;

namespace SlackSharp.Models
{
    [DataContract]
    public class Payload
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "icon_url")]
        public string IconUrl { get; set; }

        [DataMember(Name = "icon_emoji")]
        public string IconEmoji { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "attachments")]
        public Attachment[] Attachments { get; set; }

        [DataMember(Name = "link_names")]
        public string LinkNames { get; set; }
    }

    [DataContract]
    public class Attachment
    {
        [DataMember(Name = "fallback")]
        public string Fallback { get; set; }

        [DataMember(Name = "color")]
        public string Color { get; set; }

        [DataMember(Name = "pretext")]
        public string Pretext { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "fields")]
        public Field[] Fields { get; set; }

        [DataMember(Name = "mrkdwn_in")]
        public string[] MrkdwnIn { get; set; }
    }

    [DataContract]
    public class Field
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "short")]
        public bool Short { get; set; }
    }
}
