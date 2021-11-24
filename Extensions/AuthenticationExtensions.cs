using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace LaundryWebApi.Extensions
{
    public static class AuthenticationExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x=>x.Type == "id").Value;
        }
    }
}
