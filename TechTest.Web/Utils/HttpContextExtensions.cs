using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TechTest.Web.Utils
{
    public static class HttpContextExtensions
    {
        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            MyHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}
