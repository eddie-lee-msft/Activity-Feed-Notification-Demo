namespace Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ActivityNotificationPayload
    {
        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("activityType")]
        public string ActivityType { get; set; }

        [JsonProperty("previewText")]
        public string PreviewText { get; set; }

        [JsonProperty("onClickWebUrl")]
        public string OnClickWebUrl { get; set; }

        [JsonProperty("recipient")]
        public Recipient Recipient { get; set; }

        [JsonProperty("templateParameters")]
        public ICollection<KeyValuePair> TemplateParameters { get; set; }

        [JsonProperty("teamsAppId")]
        public string TeamsAppId { get; set; }
    }

    public class Recipient
    {
        [JsonProperty("@odata.type")]
        public string ODataType { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }

    public class KeyValuePair
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}