using Country.Models;

namespace Country.Helper
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}
