using System.Net;
using Core.Entities.Abstract.Exceptions;
using Core.Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Extensions
{

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            //  New exception types could be added here later

                            NotFoundException => StatusCodes.Status404NotFound,
                            ValidationException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError

                        };

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message

                        }.ToString());

                    }
                });
            });
        }
    }
}

