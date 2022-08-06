using System.Reflection;
using FluentValidation;
using MediatR;
using MeetupAPI.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace MeetupAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}