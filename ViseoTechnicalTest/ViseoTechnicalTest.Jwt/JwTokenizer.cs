using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ViseoTechnicalTest.Jwt
{
    public class JwTokenizer : IJwTokenizer
    {
        private string secretKey = "";
        private string issuer = "";
        private string audience = "";
        private IConfiguration configuration;
        public JwTokenizer(IConfiguration configuration)
        {
            this.configuration = configuration;
        
            secretKey = configuration["Jwt:SecretKey"];
            issuer = configuration["Jwt:Issuer"];
            audience = configuration["Jwt:Audience"];

        }
        public string CreateToken(string username, List<string> roles, DateTime tokenExpiration)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));      
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //create token
            var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: tokenExpiration,
                    signingCredentials: signingCredentials,
                    claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }      
    }
}
