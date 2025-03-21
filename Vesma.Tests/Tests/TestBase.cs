using Microsoft.Extensions.DependencyInjection;
using Vesma.Data;
using Vesma.Tests.Fixtures;

namespace Vesma.Tests.Tests;

public class TestBase(ServiceProviderFixture fixture) : IDisposable
{
    protected readonly ServiceProviderFixture _fixture = fixture;

    public virtual void Dispose()
    {
        using var scope = _fixture.ServiceProvider.CreateScope();
        var context = scope.GetRequiredService<VesmaDbContext>();

        context.RemoveRange(context.Vessels);
        context.RemoveRange(context.IMOs);
    }
}
