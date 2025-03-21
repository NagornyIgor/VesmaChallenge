using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vesma.Data;
using Vesma.Tests.Data;
using Vesma.Tests.Fixtures;

namespace Vesma.Tests.Tests.Vessels;

[Collection(ServiceCollectionAnnotation.CollectionDefinitionName)]
public class UpdateVesselCommandHandlerTests(ServiceProviderFixture fixture)
        : TestBase(fixture)
{
    [Fact]
    public async Task UpdateVesselCommand_Update_Success()
    {
        // Arrange

        using var scope = _fixture.ServiceProvider.CreateScope();
        var context = scope.GetRequiredService<VesmaDbContext>();
        var mediator = scope.GetRequiredService<IMediator>();

        var createCommand = DataProvider.Vessel.GenerateRegisterVesselCommand();
        var createResult = await mediator.Send(createCommand);

        // Act

        var updateCommand = DataProvider.Vessel.GenerateUpdateVesselCommand(createResult.Value.Id);
        var updateResult = await mediator.Send(updateCommand);
        // Assert

        Assert.NotNull(updateResult);
        Assert.True(updateResult.IsSuccess);
        Assert.NotNull(updateResult.ValueOrDefault);
        Assert.Equal(createResult.Value.Id, updateResult.Value.Id);
        Assert.Equal(updateCommand.Name, updateResult.Value.Name);
        Assert.Equal(updateCommand.IMO.ToLower(), updateResult.Value.IMO);
        Assert.Equal(updateCommand.Type, updateResult.Value.Type);
        Assert.Equal(updateCommand.Capacity, updateResult.Value.Capacity);
    }
}
