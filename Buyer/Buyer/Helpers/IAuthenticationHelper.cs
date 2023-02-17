using Buyer.Entities;

namespace Buyer.Helpers
{
    public interface IAuthenticationHelper
    {
            public bool AuthenticatePrincipal(Principal principal);

            public string GenerateJwt(Principal principal);
    }
}
