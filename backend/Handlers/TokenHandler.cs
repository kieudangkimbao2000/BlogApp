using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BlogApp.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.Handlers
{
    public class TokenHandler(IConfiguration configuration)
    {
        public string CreateToken(AccountDTO accountDTO) 
        {
            string secretKey = configuration["Jwt:SecrectKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("Username", accountDTO.Username),
                    new System.Security.Claims.Claim("Name", accountDTO.Name),
                    new System.Security.Claims.Claim("Email", accountDTO.Email ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpireMinutes", 60)),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}