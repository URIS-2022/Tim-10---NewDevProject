using Contract.Models;

namespace Contract.Helpers
{
        public interface IAuthenticationHelper
        {
            public bool AuthenticatePrincipal(Principal principal);

            public string GenerateJwt(Principal principal);
        }
}
