using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vesma.Core.Configuration;
using Vesma.Data;

namespace Vesma.Tests.Fixtures;

public class ServiceProviderFixture
{
    public readonly IServiceProvider ServiceProvider;

    public ServiceProviderFixture()
    {
        var services = new ServiceCollection();

        SetupDBContext(services);
        SetupCore(services);

        ServiceProvider = services.BuildServiceProvider();
    }

    private void SetupDBContext(IServiceCollection services)
    {
        services.AddDbContext<VesmaDbContext>(options =>
            options.UseInMemoryDatabase("VesmaInMemoryDb"));
    }

    private void SetupCore(IServiceCollection services)
    {
        services.ConfigureCore();

        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
    }
}
