using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API
{
    public static class BasketMinimalApi
    {
        public static void Config(WebApplication app)
        {
            app.MapGet("/cart/get", (ICartRepository cartRepository, [FromQuery] string userName) =>
            {
                return cartRepository.GetAsync(userName);
            })
            .WithName("getcart")
            .WithOpenApi();

            app.MapPost("/cart/update", async (ICartRepository cartRepository, DiscountGrpcService discountGrpcService, [FromBody] Cart cart) =>
            {
                foreach (var item in cart.Items)
                {
                    var coupon = await discountGrpcService.GetDiscount(item.ProductName);
                    item.Price -= coupon.Amount;
                }

                return cartRepository.UpdateAsync(cart);
            })
            .WithName("updatecart")
            .WithOpenApi();

            app.MapDelete("/cart/delete", (ICartRepository cartRepository, [FromQuery] string userName) =>
            {
                return cartRepository.DeleteAsync(userName);
            })
            .WithName("deletecart")
            .WithOpenApi();
        }
    }
}
