using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Consumers.Base.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class BaseConsumerTests : HostedUnitTest
{
    public BaseConsumerTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
