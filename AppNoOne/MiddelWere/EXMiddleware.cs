using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppNoOne.MiddelWere
{
    public class EXMiddleware
    {
        private readonly RequestDelegate _next;

        public EXMiddleware( RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //  await _logErrorService.CreateLog(httpContext, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            if (error != null && error is SecurityTokenException)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    StatusCode = context.Response.StatusCode,
                    Msg = "Unauthorized"
                }));
            }

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusCode = context.Response.StatusCode,
                Msg = error.Message
            }));
        }
    }
}
