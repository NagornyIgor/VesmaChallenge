using KellermanSoftware.CompareNetObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vesma.Core.Handlers.Vessels.Queries.GetVesselById;
using Vesma.Data;
using Vesma.Tests.Data;
using Vesma.Tests.Fixtures;

namespace Vesma.Tests.Tests.Vessels;

[Collection(ServiceCollectionAnnotation.CollectionDefinitionName)]
public class GetVesselByIdQueryHandlerTests(ServiceProviderFixture fixture) : TestBase(fixture)
{
    private readonly CompareLogic compareLogic = new();

    [Fact]
    public async Task GetVesselByIdQuery_GetById_Success()
    {
        // Arrange

        using var scope = _fixture.ServiceProvider.CreateScope();
        var context = scope.GetRequiredService<VesmaDbContext>();
        var mediator = scope.GetRequiredService<IMediator>();

        var createCommand = DataProvider.Vessel.GenerateRegisterVesselCommand();
        var createResult = await mediator.Send(createCommand);

        // Act

        var getResult = await mediator.Send(new GetVesselByIdQuery(createResult.Value.Id));

        // Assert

        Assert.NotNull(getResult);
        Assert.True(getResult.IsSuccess);
        Assert.NotNull(getResult.ValueOrDefault);
        Assert.Equal(createResult.Value.Id, getResult.Value.Id);

        var compareResult = compareLogic.Compare(createResult.Value, getResult.Value);
        Assert.True(compareResult.AreEqual, compareResult.DifferencesString);
    }
}
