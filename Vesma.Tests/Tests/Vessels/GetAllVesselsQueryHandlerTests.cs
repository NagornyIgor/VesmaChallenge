using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vesma.Core.Handlers.Vessels.Queries.GetAllVessels;
using Vesma.Data;
using Vesma.Tests.Data;
using Vesma.Tests.Fixtures;

namespace Vesma.Tests.Tests.Vessels;

[Collection(ServiceCollectionAnnotation.CollectionDefinitionName)]
public class GetAllVesselsQueryHandlerTests(ServiceProviderFixture fixture) : TestBase(fixture)
{
    [Theory]
    [InlineData(5)]
    public async Task GetVesselByIdQuery_GetById_Success(int vesselAmount)
    {
        // Arrange

        using var scope = _fixture.ServiceProvider.CreateScope();
        var context = scope.GetRequiredService<VesmaDbContext>();
        var mediator = scope.GetRequiredService<IMediator>();

        var tasks = Enumerable.Range(0, vesselAmount)
            .Select(x => mediator.Send(DataProvider.Vessel.GenerateRegisterVesselCommand()));

        var vessels = await Task.WhenAll(tasks);

        // Act

        var getResult = await mediator.Send(new GetAllVesselsQuery());

        // Assert

        Assert.NotNull(getResult);
        Assert.True(getResult.IsSuccess);
        Assert.NotNull(getResult.ValueOrDefault);

        Assert.All(vessels.Select(x => x.Value), vessel =>
        {
            Assert.NotNull(getResult.Value.FirstOrDefault(x => x.Id == vessel.Id));
        });
    }
}
