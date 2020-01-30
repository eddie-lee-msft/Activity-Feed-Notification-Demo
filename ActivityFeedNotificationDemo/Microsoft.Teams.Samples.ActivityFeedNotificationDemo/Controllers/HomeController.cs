namespace Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Controllers
{
    using Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Authorization;
    using Microsoft.Teams.Samples.ActivityFeedSample.Models;
    using Requests;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Mvc;
    using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
    using RouteAttribute = System.Web.Mvc.RouteAttribute;

    public class HomeController : Controller
    {
        [Route("ActivityFeedPage")]
        public ActionResult ActivityFeedPage(
            [FromUri(Name = "tenantId")] string tenantId,
            [FromUri(Name = "teamId")] string teamId,
            [FromUri(Name = "channelId")] string channelId,
            [FromUri(Name = "userId")] string userId)
        {
            ActivityFeedModel activityFeedModel = new ActivityFeedModel
            {
                TenantId = tenantId,
                TeamId = teamId,
                ChannelId = channelId,
                UserId = userId
            };

            return View("ActivityFeedPage", activityFeedModel);
        }

        [HttpPost]
        [Route("ActivityFeedPage")]
        public async Task<ActionResult> SendActivityFeedNotification(
            string tenantId,
            string teamId,
            string channelId,
            string userId)
        {
            string messagingToken = await Authorization.GetAppPermissionToken(tenantId);

            new SendActivityFeedNotificationRequest(tenantId, teamId, channelId, userId, messagingToken).SendRequest();

            ActivityFeedModel activityFeedModel = new ActivityFeedModel
            {
                TenantId = tenantId,
                TeamId = teamId,
                ChannelId = channelId,
                UserId = userId,
            };

            return View("ActivityFeedPage", activityFeedModel);
        }

        [Route("configure")]
        public ActionResult Configure()
        {
            return View();
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}