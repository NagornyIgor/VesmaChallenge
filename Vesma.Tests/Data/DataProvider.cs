using Bogus;

namespace Vesma.Tests.Data;

internal static partial class DataProvider
{
    /// <summary>
    /// Returns new instance of <see cref="Faker"/>
    /// </summary>
    public static Faker DataGenerator => new();
}
