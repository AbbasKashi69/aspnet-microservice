using EventBus.Messages.Events;
using Mapster;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;

        public BasketCheckoutConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = TypeAdapter.Adapt<CheckoutOrderCommand>(context.Message);

            int result = await _mediator.Send(command);
        }
    }
}
