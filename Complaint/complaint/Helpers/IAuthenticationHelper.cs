using complaint.Models;

namespace complaint.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);
        public string GenerateJwt(Principal principal);

    }
}
