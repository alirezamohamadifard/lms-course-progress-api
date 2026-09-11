using Lms.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lms.Infrastructure.Authentication
{
    public sealed class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generator (User user)
        {
            var Issuer = _configuration["Jwt:Issuer"] 
                ?? throw new InvalidOperationException("jwt issuer not configured");

            var Audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT audience was not configured.");

            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key Was not configured");

            var expiryInMinutes = _configuration.GetValue<int>("Jwt:ExpiryInMinutes");

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            
            var credentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
