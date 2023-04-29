namespace BackendTests;

using Backend.Src.Converters;
using Backend.Src.Db.TestFixtures;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using Backend.Src.Models;

public class BaseRepoTests : IClassFixture<DbTestFixture>
{
    public BaseRepoTests(DbTestFixture fixture)
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
    [Fact]
    public async void CreateBaseRepos()
    {
        using var context = Fixture.CreateContext();
        CityRepo cityRepo = new(context);
        var result = await cityRepo.CreateOneAsync(new City() { Name = "Espoo" });
        Assert.True(result?.Name == "Espoo");
        try
        {
            result = await cityRepo.CreateOneAsync(new City() { Name = "Tampere" });
            Assert.True(false, "Database creation did not fail");
        }
        catch
        {
            Assert.True(true);
        }
        var total = await cityRepo.GetAllAsync(null);
        Assert.True(total.Count() == 3);
    }
    [Fact]
    public async void GetAllBaseRepos()
    {
        var context = Fixture.CreateContext();
        CityRepo cityRepo = new(context);
        var result = await cityRepo.GetAllAsync(new BaseQueryOptions() { Limit = 1 });
        Assert.True(result.Count() == 1);
        result = await cityRepo.GetAllAsync(new NameFilter() { Name = "Espoo" });
        Assert.True(result.First().Name == "Espoo");
        result = await cityRepo.GetAllAsync(new NameFilter() { Name = "Tampere" });
        Assert.True(result.First().Name == "Tampere");
        result = await cityRepo.GetAllAsync(new NameFilter() { Name = "hsfdpoer" });
        Assert.True(!result.Any());
    }
}