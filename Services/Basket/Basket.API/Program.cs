using Basket.API;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddGrpcClient<DiscountProtosService.DiscountProtosServiceClient>(
    options =>
    {
        options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl"));
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

BasketMinimalApi.Config(app);

app.Run();
