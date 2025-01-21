using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


var environment = builder.Environment.EnvironmentName;
IConfigurationBuilder myconfig = new ConfigurationBuilder()
    .AddJsonFile($"ocelot.{environment}.json", true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(myconfig.Build());

var app = builder.Build();

app.MapGet("/", () => "hello werld!");

await app.UseOcelot();


app.Run();
