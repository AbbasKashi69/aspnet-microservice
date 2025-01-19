using EventBus.Messages.Common;
using MassTransit;
using Ordering.API;
using Ordering.API.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependency injection my services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


// start mass transit configuration
string hostAddress = builder.Configuration.GetValue<string>("EventBusSettings:HostAddress") ?? "";
builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<BasketCheckoutConsumer>();

    options.UsingRabbitMq((context, config) =>
    {
        config.Host(hostAddress: new Uri(hostAddress));
        config.ReceiveEndpoint(
            queueName: EventBusConstants.BasketCheckoutQueue,
            configureEndpoint: endPoint=>
            {
                endPoint.ConfigureConsumer<BasketCheckoutConsumer>(context);
            });
    });
});

builder.Services.AddScoped<BasketCheckoutConsumer>();
// end mass transit configuration

var app = builder.Build();

OrderingMinimalApi.Config(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
