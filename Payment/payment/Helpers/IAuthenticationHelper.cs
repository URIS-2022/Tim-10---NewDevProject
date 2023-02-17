using payment.Models;

namespace payment.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);
        public string GenerateJwt(Principal principal);

    }
}
