using Microsoft.IdentityModel.Tokens;
using Personality.Data;
using Personality.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Personality.Helper
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}    


