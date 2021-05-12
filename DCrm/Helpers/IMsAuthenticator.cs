using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace DCrm.Helpers
{
    public interface IMsAuthenticator
    {
        Task<string> FetchToken(string authority);
        Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri);
        void ClearAllCookies(string authority);
        Task<bool> IsAuthenticated(string authority);
    }
}
