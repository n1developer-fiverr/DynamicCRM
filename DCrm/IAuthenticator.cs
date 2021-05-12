using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace DCrm
{
    public interface IAuthenticator
    {
        Task<string> FetchToken(string authority);
        Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri);
        void ClearAllCookies();
    }

}
