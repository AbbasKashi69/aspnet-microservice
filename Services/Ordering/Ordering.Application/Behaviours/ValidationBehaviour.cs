﻿

using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                                            _validators.Select(s => s.ValidateAsync(context, cancellationToken)));

                var result = validationResults.SelectMany(s => s.Errors).Where(w => w != null).ToList();
                if (result.Count > 0)
                    throw new ValidationException(
                        message: "validation",
                        errors: result);
            }

            return await next();
        }
    }
}
