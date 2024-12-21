using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            var exist = products.Find(p => true).Any();
            if (exist is false)
            {
                products.InsertMany(GenerateData());
            }
        }

        private static IEnumerable<Product> GenerateData()
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < 25; i++)
            {
                products.Add(new Product
                {
                    Category = "category " + i,
                    Description = "description " + i,
                    Id = Guid.NewGuid().ToString(),
                    ImageFile = "image " + i,
                    Name = "nokia a " + i,
                    Price = Random.Shared.Next(1000, 98765),
                    Summary = "summary " + i
                });
            }
            return products;
        }
    }
}
