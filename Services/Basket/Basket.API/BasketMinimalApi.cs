using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
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


            app.MapPost("/cart/checkout", async (ICartRepository cartRepository, IPublishEndpoint publishEndpoint, [FromBody] BasketCheckout checkout) =>
            {
                // 1.get existing basket with total price

                // 2.create BasketCheckoutEvent - set total price on chckeout event message

                // 3.send checkout event to rabbitmq

                // 4.remove basket

                var basket = new Cart();
                //var basket = await cartRepository.GetAsync(checkout.UserName);
                //if (basket is null)
                //    return Results.BadRequest();

                var eventMessage = TypeAdapter.Adapt<BasketCheckoutEvent>(checkout);
                eventMessage.TotalPrice = basket.TotalPrice;

                await publishEndpoint.Publish(eventMessage);

                await cartRepository.DeleteAsync(checkout.UserName);

                return Results.Ok();
            })
            .WithName("checkoutcart")
            .WithOpenApi();
        }
    }
}
