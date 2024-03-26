using Catalog.Models.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Catalog.Extensions
{
    //essa seria um método de extensão para criarmos exceções personalizadas utilizando middlewares(verificar no program)
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

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Trace = contextFeature.Error.StackTrace

                        }.ToString()) ;
                    }
                    
                });
            });
        }
    }
}
