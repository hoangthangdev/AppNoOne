using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppNoOne.MiddelWere
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(IOptions<JwtSettings> jwtSettings, RequestDelegate next)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _jwtSettings = jwtSettings.Value;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                var token = authHeader.Substring("Bearer".Length).Trim();
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Items["Message"] = "401 Unauthorized";
                    return;
                }
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken validatedToken;
                try
                {
                    var claimsPrincipal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                    context.User = claimsPrincipal;
                }
                catch (SecurityTokenExpiredException)
                {
                    // Xử lý lỗi token hết hạn và trả về mã trạng thái HTTP 401 Unauthorized
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                catch (SecurityTokenValidationException)
                {
                    // Xử lý lỗi token không hợp lệ và trả về mã trạng thái HTTP 401 Unauthorized
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                catch (Exception)
                {
                    // Xử lý các ngoại lệ khác và trả về mã trạng thái HTTP 500 Internal Server Error
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            
            await _next(context);
        }
    }
}
