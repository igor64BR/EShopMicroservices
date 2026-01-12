using BuildingBlocks.Behavior;
using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services here
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline here
app.ConfigureGlobalExceptionHandling();
app.ConfigureSwagger();

app.MapCarter();

app.Run();
