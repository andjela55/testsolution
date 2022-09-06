using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using System.Net;

namespace WebApi.Middleware
{
   
        public static class ExceptionMiddleware
        {
            public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
            {
                app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.ContentType = "text/javascript";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (contextFeature?.Error is BadRequestException)
                        {
                            var ex = contextFeature?.Error;
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(ex.Message);
                        }
                        else if (contextFeature?.Error is Exception)
                        {
                            var ex = contextFeature?.Error;
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            logger.LogError($"Error {context.Response.StatusCode} - {ex?.Message}");
                        }
                    });
                });
            }

        }
}
