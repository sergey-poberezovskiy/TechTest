﻿using Microsoft.AspNetCore.Http;

namespace TechTest.Web.Utils
{
    public class MyHttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static HttpContext Current => _httpContextAccessor.HttpContext;

        public static string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }
    }
}
