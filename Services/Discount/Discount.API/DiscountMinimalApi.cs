
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API
{
    public static class DiscountMinimalApi
    {
        public static void Config(WebApplication app)
        {
            app.MapGet("/discount/get", (IDiscountRepository discountRepository, [FromQuery] string productName) =>
            {
                return discountRepository.GetAsync(productName);
            })
            .WithName("getdiscount")
            .WithOpenApi();

            app.MapPost("/discount/create", (IDiscountRepository discountRepository, [FromBody] Coupon coupon) =>
            {
                return discountRepository.CreateAsync(coupon);
            })
            .WithName("creatediscount")
            .WithOpenApi();

            app.MapPut("/discount/update", (IDiscountRepository discountRepository, [FromBody] Coupon coupon) =>
            {
                return discountRepository.UpdateAsync(coupon);
            })
            .WithName("updatediscount")
            .WithOpenApi();

            app.MapDelete("/discount/delete", (IDiscountRepository discountRepository, [FromQuery] string productName) =>
            {
                return discountRepository.DeleteAsync(productName);
            })
            .WithName("deletediscount")
            .WithOpenApi();
        }
    }
}
