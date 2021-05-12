using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Webkit;
using DCrm.Helpers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

[assembly: Dependency(typeof(DCrm.Droid.Authenticator))]
namespace DCrm.Droid
{
    public class Authenticator : IMsAuthenticator
    {
        async Task<AuthenticationResult> IMsAuthenticator.Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters((Activity)Forms.Context);
            var authResult =  authContext.AcquireTokenAsync(resource, clientId, uri, platformParams).Result;
            return authResult;
        }

        void IMsAuthenticator.ClearAllCookies(string authority)
        {
            var authContext = new AuthenticationContext(authority);
            Task.Factory.StartNew(() => {
                authContext.TokenCache.Clear();
            }).Wait();
            CookieManager cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
            cookieManager.Flush();
        }

        public async Task<string> FetchToken(string authority)
        {
            try
            {
                return
                    (new AuthenticationContext(authority))
                        .TokenCache
                        .ReadItems()
                        .FirstOrDefault(x => x.Authority == authority).AccessToken;
            }
            catch (Exception ex)
            {
                //ex.WriteFormattedMessageToDebugConsole(this);
            }
            return null;
        }


        public async Task<bool> IsAuthenticated(string authority)
        {
            return (await FetchToken(authority)) != null;
        }
    }
}
