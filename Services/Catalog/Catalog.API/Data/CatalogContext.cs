using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString") ?? string.Empty;
            var client = new MongoClient();

            string databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName") ?? string.Empty;
            var database = client.GetDatabase(databaseName);

            string collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName") ?? string.Empty;
            Products = database.GetCollection<Product>(collectionName);

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
