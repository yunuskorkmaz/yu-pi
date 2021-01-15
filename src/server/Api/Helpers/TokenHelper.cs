using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers
{
    public class TokenHelper
    {
        public IConfiguration Configuration { get; set; }

        public TokenHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IEnumerable<Claim> SetClaims(User model)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("email",model.Email));
            claims.Add(new Claim("name",model.Name));
            claims.Add(new Claim("surname",model.Surname));
            return claims;
        }

        public string CreateAccessToken(User model)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: DateTime.Now.AddMinutes(60),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(securityToken);
        }
    }
}