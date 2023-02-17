using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using User1.Data;
using User1.Entities;
using User1.Models;

namespace User1.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IUserRespository userRespository;
        private readonly UserContext context;

        public AuthenticationHelper(IConfiguration configuration, IUserRespository userRespository, UserContext context)
        {
            this.configuration = configuration;
            this.userRespository = userRespository;
            this.context = context;
        }

        public bool AuthenticatePrincipal(Principal principal)
        {
            if (userRespository.UserWithCredentialsExists(principal.Username, principal.Password))
            {
                return true;
            }

            return false;
        }

        public string GenerateJwt(Principal principal)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             null,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            User user = context.User.FirstOrDefault(e => e.username == principal.Username);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
