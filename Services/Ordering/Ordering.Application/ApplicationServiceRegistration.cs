

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Application.Behaviours;
using Ordering.Application.Contracts.Infrastructure;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //mediatr 
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(options=>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                //options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //fluent validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
