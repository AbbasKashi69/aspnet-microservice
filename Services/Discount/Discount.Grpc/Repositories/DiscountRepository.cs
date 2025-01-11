using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection CreateConnection() =>
            new NpgsqlConnection(
                connectionString: _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        public async Task<Coupon?> GetAsync(string productName)
        {
            using var connection = CreateConnection();

            Coupon? coupon = await connection.QueryFirstOrDefaultAsync(
                sql: "select * from coupon where ProductName = @ProductName",
                param: new { ProductName = productName });

            return coupon;
        }

        public async Task<bool> CreateAsync(Coupon coupon)
        {
            using var connection = CreateConnection();

            var rows = await connection.ExecuteAsync(
                sql: "insert into coupon (ProductName, Description, Amount) values (@ProductName, @Description, @Amount)",
                param: new
                {
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount
                });

            return rows > 0;
        }

        public async Task<bool> UpdateAsync(Coupon coupon)
        {
            using var connection = CreateConnection();

            var rows = await connection.ExecuteAsync(
                sql: "update coupon set ProductName = @ProductName, Description = @Description, Amount = @Amount) Where Id = @Id",
                param: new
                {
                    coupon.Id,
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount
                });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(string productName)
        {
            using var connection = CreateConnection();

            var rows = await connection.ExecuteAsync(
                sql: "delete from coupon ProductName = @ProductName",
                param: new { ProductName = productName });

            return rows > 0;
        }
    }
}
