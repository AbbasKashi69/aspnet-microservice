using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);


var environment = builder.Environment.EnvironmentName;
IConfigurationBuilder myconfig = new ConfigurationBuilder()
    .AddJsonFile($"ocelot.{environment}.json", true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(myconfig.Build())
    .AddCacheManager(x => x.WithDictionaryHandle());

var app = builder.Build();

await app.UseOcelot();


app.Run();
