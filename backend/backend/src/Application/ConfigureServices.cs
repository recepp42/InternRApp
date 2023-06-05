using System.Reflection;
using AutoMapper;
using backend.Application.Common.Behaviours;
using backend.Application.InternShips.Queries.GetAllInternShips;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using static backend.Application.InternShips.Queries.GetAllInternShips.InternShipListDto;

namespace backend.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        return services;
    }
}
