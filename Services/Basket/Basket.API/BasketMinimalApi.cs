using Basket.API.Entities;
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

            app.MapPost("/cart/update", (ICartRepository cartRepository, [FromBody] Cart cart) =>
            {
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
