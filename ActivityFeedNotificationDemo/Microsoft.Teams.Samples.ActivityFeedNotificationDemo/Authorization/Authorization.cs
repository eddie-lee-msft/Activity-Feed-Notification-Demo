namespace Microsoft.Teams.Samples.ActivityFeedNotificationDemo.Authorization
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;

    public static class Authorization
    {
        public static void DetermineAuthentication(HttpCookieCollection requestCookies)
        {
            string userToken = GetTokenFromCookie(requestCookies);

            if (userToken == null)
                FailAuth(requestCookies);

            // For debugging sanity, make sure the cookie is from the app ID you are actually 
            // expecting, and not a leftover of a previous version of your app.
            if (GetTokenClaim(userToken, "appid") != GetGraphAppId())
                FailAuth(requestCookies);
        }

        public static string GetTokenFromCookie(HttpCookieCollection cookies)
        {
            var cookie = cookies["GraphToken"];

            if (cookie == null)
                return null;
            else
                return cookie.Value;
        }

        private static void FailAuth(HttpCookieCollection requestCookies)
        {
            if (requestCookies["GraphToken"] != null)
            {
                // Remove invalid cookie by expiring it
                requestCookies["GraphToken"].Expires = DateTime.Now.AddDays(-1);
                requestCookies["GraphToken"].Value = "invalid";
            }

            throw new Exception("Unauthorized user!");
        }

        private static string GetTokenClaim(string token, string claimType)
        {
            var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().ReadJwtToken(token);
            foreach (var claim in jwt.Claims)
            {
                if (claim.Type == claimType)
                    return claim.Value;
            }

            return null;
        }

        public static async Task<string> GetAppPermissionToken(string tenant, Nullable<bool> useRSC)
        {
            string appId = GetGraphAppId();
            string appSecret = Uri.EscapeDataString(GetGraphAppPassword());

            string response = await HttpHelpers.POST($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token",
                    $"grant_type=client_credentials&client_id={appId}&client_secret={appSecret}"
                    + "&scope=https%3A%2F%2Fcanary.graph.microsoft.com%2F.default");
            string token = response.Deserialize<TokenResponse>().access_token;
            return token;
        }

        public static void ProcessAadCallbackAndStoreUserToken(HttpContextBase httpContext, HttpCookieCollection cookies)
        {
            string req_txt;
            using (StreamReader reader = new StreamReader(httpContext.Request.InputStream))
            {
                req_txt = reader.ReadToEnd();
            }
            string token = Authorization.ParseOauthResponse(req_txt);
            cookies.Add(new System.Web.HttpCookie("GraphToken", token));

            // Ideally we would store the token on the server and never send it to the 
            // client, since in this app the client doesn't make Graph calls directly. 
            // But it's not a big deal to send the user delegated token down, 
            // and if the client app actually used it we would do so without reservation.
            // Never put an application permissions token in a cookie, though.
        }

        // Also store the token in a cookie so the client can pass it back to us later
        private static string ParseOauthResponse(string oathResponse)
        {
            Debug.Assert(oathResponse.Split('&')[0].Split('=')[0] == "access_token");
            return oathResponse.Split('&')[0].Split('=')[1];
        }

        // Returns the appid for user delegated use
        public static string GetGraphAppId()
        {
            return ConfigurationManager.AppSettings["GraphNoRSCAppId"];
        }

        // Returns the appid for user delegated use
        public static string GetGraphAppPassword()
        {
            return ConfigurationManager.AppSettings["GraphNoRSCAppPassword"];
        }
    }
}