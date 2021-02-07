using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using yu_pi.Domain.Entities;

namespace yu_pi.Helpers
{
    public class JwtTokenGenerator
    {
        public IConfiguration Configuration { get; set; }

        public JwtTokenGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IEnumerable<Claim> SetClaims(User model)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("email", model.Email));
            claims.Add(new Claim("name", model.Name));
            claims.Add(new Claim("surname", model.Surname));
            return claims;
        }

        public string CreateAccessToken(User model)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["token:signingKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["token:issuer"],
                audience: Configuration["token:audience"],
                expires: DateTime.Now.AddMinutes(60),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: SetClaims(model)
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(securityToken);
        }
    }
}