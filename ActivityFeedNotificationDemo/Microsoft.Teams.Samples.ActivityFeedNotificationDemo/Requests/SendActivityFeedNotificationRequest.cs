namespace Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Requests
{
    using Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Configuration;

    public class SendActivityFeedNotificationRequest
    {
        private const string Endpoint = 
            "https://canary.graph.microsoft.com/testprodbetatestTeamsGraphSvcDev/teamwork/generateActivityNotification";

        private readonly string tenantId;
        private readonly string teamId;
        private readonly string channelId;
        private readonly string userId;
        private readonly string authorizationToken;

        public SendActivityFeedNotificationRequest(
            string tenantId,
            string teamId,
            string channelId,
            string userId,
            string authorizationToken)
        {
            this.tenantId = tenantId;
            this.teamId = teamId;
            this.channelId = channelId;
            this.userId = userId;
            this.authorizationToken = authorizationToken;
        }

        public async void SendRequest()
        {
            await HttpHelpers.PostWithAuth(
                Endpoint,
                ConstructPayload(),
                this.authorizationToken);
        }

        private string ConstructPayload()
        {
            ActivityNotificationPayload payload = new ActivityNotificationPayload
            {
                About = $"https://graph.microsoft.com/beta/teams/{this.teamId}/channels/{this.channelId}",
                ActivityType = "taskCreated",
                PreviewText = "New Task Created",
                OnClickWebUrl = $"https://teams.microsoft.com/l/channel/{this.channelId}/test?groupId={this.teamId}&tenantId={this.tenantId}",
                Recipient = new Recipient
                {
                    ODataType = "microsoft.graph.aadUserNotificationAudience",
                    UserId = this.userId
                },
                TemplateParameters = new List<KeyValuePair> { new KeyValuePair { Name = "taskId", Value = "Task 12322" } },
                TeamsAppId = ConfigurationManager.AppSettings["TeamsAppId"]
            };

            return JsonConvert.SerializeObject(payload);
        }
    }
}