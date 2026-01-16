using BuildingBlocks.Behavior;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services here
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.ConfigureSwagger();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline here
app.UseExceptionHandler(opt => { });
app.ConfigureSwagger();

app.MapCarter();

app.Run();
