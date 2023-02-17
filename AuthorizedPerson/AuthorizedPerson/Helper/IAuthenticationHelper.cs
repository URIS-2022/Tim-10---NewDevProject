using AuthorizedPerson.Entities;

namespace AuthorizedPerson.Helper
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}
