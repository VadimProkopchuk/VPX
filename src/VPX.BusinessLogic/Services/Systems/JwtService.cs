using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VPX.BusinessLogic.Core.Contracts.Systems;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VPX.Domain;
using VPX.Models;
using VPX.Models.Settings;

namespace VPX.BusinessLogic.Services.Systems
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
            var nowUtc = systemTimeService.GetDateUtc();
            var expiresAt = nowUtc.AddDays(jwtSettings.LifeTimeInDays);
            var tokenDescriptor = BuildSecurityTokenDescriptor(user, nowUtc, expiresAt);
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new JwtModel
            {
                ExpiredAt = expiresAt,
                Token = token
            };
        }

        private SecurityTokenDescriptor BuildSecurityTokenDescriptor(User user, DateTime nowUtc, DateTime expires)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

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
                Expires = expires,
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
