using Library.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Library.API.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appErr =>
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is not null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    });
                }
            });
        });
    }
}