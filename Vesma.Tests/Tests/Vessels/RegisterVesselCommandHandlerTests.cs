using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vesma.Data;
using Vesma.Tests.Data;
using Vesma.Tests.Fixtures;

namespace Vesma.Tests.Tests.Vessels;

[Collection(ServiceCollectionAnnotation.CollectionDefinitionName)]
public class RegisterVesselCommandHandlerTests(ServiceProviderFixture fixture) : TestBase(fixture)
{
    [Fact]
    public async Task RegisterVesselCommand_Create_Success()
    {
        // Arrange

        using var scope = _fixture.ServiceProvider.CreateScope();
        var context = scope.GetRequiredService<VesmaDbContext>();
        var mediator = scope.GetRequiredService<IMediator>();

        var command = DataProvider.Vessel.GenerateRegisterVesselCommand();

        // Act

        var result = await mediator.Send(command);

        // Assert

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.ValueOrDefault);
        Assert.NotEqual(default, result.Value.Id);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.Equal(command.IMO.ToLower(), result.Value.IMO);
        Assert.Equal(command.Type, result.Value.Type);
        Assert.Equal(command.Capacity, result.Value.Capacity);
    }
}
