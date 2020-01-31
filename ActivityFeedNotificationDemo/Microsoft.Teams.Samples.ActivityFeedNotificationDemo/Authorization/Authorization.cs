namespace Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Authorization
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using System.Web;

    public static class Authorization
    { 
        public static string GetTokenFromCookie(HttpCookieCollection cookies)
        {
            var cookie = cookies["GraphToken"];
            return cookie?.Value;
        }

        public static async Task<string> GetAppPermissionToken(string tenant)
        {
            string appId = GetGraphAppId();
            string appSecret = Uri.EscapeDataString(GetGraphAppPassword());

            string response = await HttpHelpers.POST($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token",
                    $"grant_type=client_credentials&client_id={appId}&client_secret={appSecret}"
                    + "&scope=https%3A%2F%2Fcanary.graph.microsoft.com%2F.default");
            string token = response.Deserialize<TokenResponse>().access_token;
            return token;
        }

        // Returns the appid for user delegated use
        public static string GetGraphAppId()
        {
            return ConfigurationManager.AppSettings["GraphAppId"];
        }

        // Returns the appid for user delegated use
        public static string GetGraphAppPassword()
        {
            return ConfigurationManager.AppSettings["GraphAppPassword"];
        }
    }
}