using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vesma.Core.Behaviours;
using Vesma.Core.Mappers;

namespace Vesma.Core.Configuration;

public static class ConfigureServices
{
    public static void ConfigureCore(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
        services.AddSingleton<AppMapper>();

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}
