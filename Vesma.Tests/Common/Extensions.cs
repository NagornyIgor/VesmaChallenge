using Microsoft.Extensions.DependencyInjection;

namespace Vesma.Tests;

internal static class Extensions
{
    public static TService GetRequiredService<TService>(this IServiceScope? scope)
    {
        ArgumentNullException.ThrowIfNull(scope);

        return scope.ServiceProvider.GetService<TService>()
            ?? throw new Exception($"Can't get required service from service provider: {typeof(TService).Name}");
    }
}
