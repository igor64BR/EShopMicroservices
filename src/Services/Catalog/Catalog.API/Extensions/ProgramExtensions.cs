using Catalog.API.Data;

namespace Catalog.API.Extensions;

public static class ProgramExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void ConfigureSwagger()
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public void ConfigureMarten()
        {
            builder.Services.AddMarten(cfg =>
            {
                cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
            }).UseLightweightSessions();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }
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
    }
}
