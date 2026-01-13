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
    }
}
