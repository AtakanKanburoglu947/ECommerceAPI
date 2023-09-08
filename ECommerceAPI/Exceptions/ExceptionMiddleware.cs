using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Http;

namespace ECommerceCore.Exceptions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(error =>
            {

                error.Run(async context =>
                {
                    ILogger logger = loggerFactory.CreateLogger(nameof(ConfigureExceptionHandler));
                    context.Response.ContentType = "application/json";
                    IExceptionHandlerFeature? contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    IHttpRequestFeature? contextRequest = context.Features.Get<IHttpRequestFeature>();
                    if (contextFeature != null)
                    {
                        string errorVM = new ErrorVM()
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = contextFeature?.Error.Message
                        }.ToString();
                        logger.LogError(errorVM);
                        await context.Response.WriteAsync(errorVM);
                    }

                });
            });  
        }
    }
}
