using System.Collections.Generic;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.Domain;
using JML.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JML.Models.Settings;

namespace JML.BusinessLogic.Services.Systems
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings jwtSettings;
        private readonly ISystemTimeService systemTimeService;

        public JwtService(IOptions<AppSettings> options,
            ISystemTimeService systemTimeService)
        {
            jwtSettings = options.Value.Jwt;
            this.systemTimeService = systemTimeService;
        }

        public JwtModel GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = BuildSecurityTokenDescriptor(user);
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new JwtModel
            {
                ExpiredAt = tokenDescriptor.Expires.Value,
                Token = token
            };
        }

        private SecurityTokenDescriptor BuildSecurityTokenDescriptor(User user)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var nowUtc = systemTimeService.GetDateUtc();
            var expiresAt = nowUtc.AddDays(jwtSettings.LifeTimeInDays);
            var roleClaims = GetRoleClaims(user);
            var claims = roleClaims.Union(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                NotBefore = nowUtc,
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        private IEnumerable<Claim> GetRoleClaims(User user)
        {
            var userRoles = user.UserRoles.Select(x => x.Role);

            foreach (var userRole in userRoles)
            {
                yield return new Claim(ClaimTypes.Role, userRole.ToString());
            }
        }
    }
}
