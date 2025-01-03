using Discount.API;
using Discount.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//for migration
using var scope = app.Services.CreateScope();
var connection = new Npgsql.NpgsqlConnection(
    connectionString: builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
try
{
    await connection.OpenAsync();

    var command = connection.CreateCommand();
    command.CommandType = System.Data.CommandType.Text;

    command.CommandText = "Drop Table If Exist coupon ";
    command.ExecuteNonQuery();

    command.CommandText = @"Create Table coupon(Id Serial Primary Key,
                                                ProductName varchar(100) Not Null,
                                                Description text,
                                                Amount int Not Null)";
    command.ExecuteNonQuery();

    command.CommandText = "insert into coupon (ProductName, Description, Amount) Values ('samsung', 'phone', 1000)";
    command.ExecuteNonQuery();

    command.CommandText = "insert into coupon (ProductName, Description, Amount) Values ('xiaomi', 'phone', 1500)";
    command.ExecuteNonQuery();
}
catch(Npgsql.NpgsqlException ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    await connection.CloseAsync();
}


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

DiscountMinimalApi.Config(app);

app.Run();
