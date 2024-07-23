using DOTA2TierList.API.Middlewares;

namespace DOTA2TierList.API.ServiceExtention
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
