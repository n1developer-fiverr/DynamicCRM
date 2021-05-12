using System;
using DCrm.Helpers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace DCrm.Services
{
    public class AdAuthenticationService
    {
        private IMsAuthenticator msAuthenticator; 
        public AdAuthenticationService()
        {
            msAuthenticator = DependencyService.Get<IMsAuthenticator>();
        }

        public bool IsAuthenticated => msAuthenticator.IsAuthenticated(App.Authority).Result;

        public AuthenticationResult Authenticate() => msAuthenticator.Authenticate(App.Authority, App.Resource, App.ClientId, App.ReturnUri).Result;

        public void Logout() => msAuthenticator.ClearAllCookies(App.Authority);

        public string AccessToken => msAuthenticator.FetchToken(App.Authority).Result;
    }
}
