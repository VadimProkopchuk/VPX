using JML.BusinessLogic.Core.Contracts.Systems;
using JML.Domain;
using JML.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JML.BusinessLogic.Services.Systems
{
    public class JwtService : IJwtService
    {
        private readonly string jwtSecret;
        private readonly ISystemTimeService systemTimeService;

        public JwtService(IOptions<AppSettings> options,
            ISystemTimeService systemTimeService)
        {
            jwtSecret = options.Value.JwtSecret;
            this.systemTimeService = systemTimeService;
        }

        public JwtModel GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var expiresAt = systemTimeService.GetDateUtc().AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new JwtModel
            {
                ExpiredAt = expiresAt,
                Token = token
            };
        }
    }
}
