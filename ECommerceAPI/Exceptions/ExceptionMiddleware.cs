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

namespace ECommerceCore.Exceptions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {

                error.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();
                    await context.Response.WriteAsync(new ErrorVM()
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = contextFeature.Error.Message
                    }.ToString());
                });
            });  
        }
    }
}
