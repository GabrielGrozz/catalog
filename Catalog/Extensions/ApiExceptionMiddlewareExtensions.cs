using System.Net;

namespace Catalog.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
                                                    //esse "this" indica que esse método é um método de extensão para a interface IApplicationBuilder
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appErr =>
            {
                appErr.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    
                });
            });
        }
    }
}
