
using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
namespace JwtMiddleware
{
    public class MyJwtService
    {
        private string mysecret;
        private string myexpDate;
        private string? myAudience;
        private readonly string? myIssuer;

        public MyJwtService(IConfiguration config)
        {
            mysecret = config.GetSection("JwtConfig").GetSection("secret").Value;
            myexpDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
            myIssuer = config.GetSection("JwtConfig").GetSection("Issuer").Value; ;
            myAudience = config.GetSection("JwtConfig").GetSection("Audience").Value; 
        }

        public string GenerateSecurityToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(mysecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)

                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(myexpDate)),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }



}

