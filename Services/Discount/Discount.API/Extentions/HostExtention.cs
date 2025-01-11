namespace Discount.API.Extentions
{
    public static class HostExtention
    {
        public static void MigrateDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var connection = new Npgsql.NpgsqlConnection(
                connectionString: app.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            try
            {
                connection.Open();

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
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
