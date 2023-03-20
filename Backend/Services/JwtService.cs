using Carwale.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Carwale.Services
{
    public class JwtService
    {
        private readonly JwtSetting _jwt;
        public JwtService(IOptions<AppSettings> appSettings) 
        {
            _jwt = appSettings.Value.Jwt;
        }
        
        public string GenerateJwtToken(CwUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UId", user.UId),
                    new Claim("Name", user.Name),
                    new Claim("UserName", user.UserName),
                    new Claim("TenantName", user.Tenant.Name),
                    new Claim("TenantUId", user.Tenant.UId),
                    new Claim("TenantId", user.TenantId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = _jwt.Audience,
                Issuer = _jwt.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
