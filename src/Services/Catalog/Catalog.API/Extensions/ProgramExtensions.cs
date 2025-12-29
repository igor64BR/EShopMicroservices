using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Catalog.API.Extensions;

public static class ProgramExtensions
{
    extension(IServiceCollection services)
    {
        public void ConfigureSwagger()
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }

    extension(WebApplication app)
    {
        public void ConfigureSwagger()
        {
            if (!app.Environment.IsDevelopment())
                return;

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Catalog API Explorer";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        public void ConfigureGlobalExceptionHandling() => app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;

            if (exception is ProductNotFoundException productNotFound)
            {
                context.Response.ContentType = Application.Json;
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                var payload = new
                {
                    error = productNotFound.Message,
                    code = "ProductNotFound"
                };

                await context.Response.WriteAsJsonAsync(payload);

                return;
            }

            if (exception is null)
            {
                context.Response.ContentType = Application.Json;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var payload = new
                {
                    error = "Unexpected error with no description happened. Please contact our support.",
                    code = "NullErrorException"
                };

                await context.Response.WriteAsJsonAsync(payload);

                return;
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (!app.Environment.IsDevelopment())
            {
                context.Response.ContentType = Application.Json;

                var payload = new
                {
                    error = "Internal server error. Please contact our support.",
                    code = "UnhandledException"
                };

                await context.Response.WriteAsJsonAsync(payload);

                return;
            }

            context.Response.ContentType = Text.Plain;

            await context.Response.WriteAsync(exception.ToString());
        }));
    }
}
