namespace Vesma.Tests.Fixtures;

[CollectionDefinition(CollectionDefinitionName)]
public class ServiceCollectionAnnotation : ICollectionFixture<ServiceProviderFixture>
{
    public const string CollectionDefinitionName = "ServiceCollectionAnnotation";

    // This class has no code, and is never created.
    // Its purpose is to be the place to apply [CollectionDefinition]
    // and all the ICollectionFixture<> interfaces.
}
