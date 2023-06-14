using DTO.OutDTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppNoOne.MiddelWere
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly JwtSettings _jwtSettings;

        public JwtAuthenticationManager(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string CreateToken(UserDTO user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Roles)
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpireTime),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
