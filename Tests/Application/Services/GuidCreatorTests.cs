using Application.Services;

namespace Tests.Application.Services;
public class GuidCreatorTests
{
    [Fact]
    public void CreatesGuid()
    {
        var service = new GuidCreator();

        var guid = service.Create();

        Assert.NotNull(guid);
        Assert.True(guid.Length > 10);
    }

    [Fact]
    public void CreatesDifferentGuids()
    {
        var service = new GuidCreator();

        var guid = service.Create();
        var guid2 = service.Create();

        Assert.NotEqual(guid, guid2);
    }
}
