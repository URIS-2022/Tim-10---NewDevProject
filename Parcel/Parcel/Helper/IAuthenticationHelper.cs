using Parcel.Models;

namespace Parcel.Helper
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}
