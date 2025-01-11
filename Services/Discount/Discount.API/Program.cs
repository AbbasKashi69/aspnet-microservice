using Discount.API;
using Discount.API.Extentions;
using Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//for migration
HostExtention.MigrateDatabase(app);

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

DiscountMinimalApi.Config(app);

app.Run();
