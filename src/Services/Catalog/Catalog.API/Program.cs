using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services here
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP requestpipeline here
app.ConfigureSwagger();
app.MapCarter();

app.Run();
