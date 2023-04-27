namespace BackendTests;

using Backend.Src.Db.TestFixtures;

public class UserTests : IClassFixture<DbTestFixture>
{
    public UserTests(DbTestFixture fixture)
    {
        Fixture = fixture;
    }
    public DbTestFixture Fixture { get; }

    [Fact]
    public void Initialized()
    {
        using var context = Fixture.CreateContext();
        Assert.True(context.Genres.Any());
        Assert.True(context.Instruments.Any());
        Assert.True(context.Cities.Any());
    }
}