
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API
{
    public static class ProductMinimalApi
    {
        public static void Config(WebApplication app)
        {
            app.MapGet("/GetProducts", (IProductRepository _productRepository) =>
            {
                return _productRepository.GetProducts();
            })
            .WithName("getproducts")
            .WithOpenApi();

            app.MapGet("/GetProductByName", (IProductRepository _productRepository, string name) =>
            {
                return _productRepository.GetProductByName(name);
            })
            .WithName("getproductbyname")
            .WithOpenApi();

            app.MapGet("/GetProductById", (IProductRepository _productRepository, string id) =>
            {
                return _productRepository.GetProductById(id);
            })
            .WithName("getproductbyid")
            .WithOpenApi();

            app.MapGet("/GetProductsByCategory", (IProductRepository _productRepository, string category) =>
            {
                return _productRepository.GetProductsByCategory(category);
            })
            .WithName("getproductbycategory")
            .WithOpenApi();

            app.MapPost("/Create", (IProductRepository _productRepository, [FromBody] Product product) =>
            {
                return _productRepository.Create(product);
            })
            .WithName("create")
            .WithOpenApi();

            app.MapPut("/Update", (IProductRepository _productRepository, [FromBody] Product product) =>
            {
                return _productRepository.Update(product);
            })
            .WithName("update")
            .WithOpenApi();

            app.MapDelete("/Delete", (IProductRepository _productRepository, [FromQuery] string id) =>
            {
                return _productRepository.Delete(id);
            })
            .WithName("delete")
            .WithOpenApi();
        }
    }
}
