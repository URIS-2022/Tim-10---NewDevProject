﻿using Contract.Data;
using Contract.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Contract.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthenticationHelper(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        public bool AuthenticatePrincipal(Principal principal)
        {
            if (userRepository.UserWithCredentialsExists(principal.username, principal.password))
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
                                             expires: DateTime.Now.AddMinutes(150),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}