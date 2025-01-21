using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator
{
    public static class ShoppingMinimalApi
    {
        public static void Config(WebApplication app)
        {
            app.MapGet("/getshopping",
                async (ICatalogService catalogService,
                IBasketService basketService,
                IOrderService orderService,
                [FromQuery] string userName) =>
            {
                var basket = await basketService.GetBasket(userName);

                foreach (var item in basket.Items)
                {
                    var product = await catalogService.GetCatalog(item.ProductId);

                    item.ProductName = product.Name;
                    item.Category = product.Category;
                    item.Summary = product.Summary;
                    item.Description = product.Description;
                    item.ImageFile = product.ImageFile;
                }

                var orders = await orderService.GetOrderByUserName(userName);

                var shoppingModel = new ShoppingModel
                {
                    UserName = userName,
                    BasketWithProduct = basket,
                    Orders = orders
                };

                return shoppingModel;
            })
            .WithName("getshopping")
            .WithOpenApi();

        }
    }
}
