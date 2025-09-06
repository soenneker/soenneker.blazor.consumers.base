using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.Consumers.Base.Tests;

[Collection("Collection")]
public class BaseConsumerTests : FixturedUnitTest
{
    public BaseConsumerTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
