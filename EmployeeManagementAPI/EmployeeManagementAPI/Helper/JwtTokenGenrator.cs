﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Api.Helper
{
    public class JwtTokenGenrator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenrator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object GenerateJSONWebToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
             {
                new Claim(ClaimTypes.Name, username)
           
            };

            var token = new JwtSecurityToken(
                jwtSettings["Issuer"],
                jwtSettings["Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: creds);
            return (new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
