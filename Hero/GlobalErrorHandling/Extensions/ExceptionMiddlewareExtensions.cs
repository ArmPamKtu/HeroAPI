using Hero.GlobalErrorHandling.Models;
using Logic.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hero.GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        private static ILogger _logger;

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("ExceptionMiddlewareLogger");
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        _logger.LogError($"Something went wrong: {contextFeature.Error}");

                        if (contextFeature.Error.GetType() == typeof(BusinessException))
                        {
                            var exception = (BusinessException)contextFeature.Error;
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)exception.Code,
                            }.ToString());
                        }

                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                            }.ToString());
                        }
                    }
                });
            });
        }
    }
}
