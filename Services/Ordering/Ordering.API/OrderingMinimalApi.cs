
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API
{
    public static class OrderingMinimalApi
    {
        public static void Config(WebApplication app)
        {
            app.MapGet("/order/getall", async (IMediator mediator, [FromQuery] string userName) =>
            {
                return await mediator.Send(new GetOrdersListQuery { UserName = userName });
            })
            .WithName("getallorders")
            .WithOpenApi();

            app.MapPost("/order/checkout", async (IMediator mediator, [FromBody] CheckoutOrderCommand checkout) =>
            {
                return await mediator.Send(checkout);
            })
             .WithName("checkoutorder")
             .WithOpenApi();

            app.MapPut("/order/update", async (IMediator mediator, [FromBody] UpdateOrderCommand order) =>
            {
                await mediator.Send(order);
            })
             .WithName("updateorder")
             .WithOpenApi();

            app.MapDelete("/order/delete", async (IMediator mediator, [FromBody] DeleteOrderCommand order) =>
            {
                await mediator.Send(order);
            })
             .WithName("deleteorder")
             .WithOpenApi();
        }
    }
}
