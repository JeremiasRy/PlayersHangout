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
        context.Database.BeginTransaction();
        
        // city
        
        CityRepo cityRepo = new(context);
        var cityResult = await cityRepo.CreateOneAsync(new City() { Name = "Espoo" });
        context.ChangeTracker.Clear();
        Assert.True(cityResult?.Name == "Espoo");
           
        cityResult = await cityRepo.CreateOneAsync(new City() { Name = "Tampere" });
        context.ChangeTracker.Clear();
        Assert.True(cityResult is null);
        var total = await cityRepo.GetAllAsync(null);
        Assert.True(total.Count() == 3);

        // instrument

        InstrumentRepo instrumentRepo = new(context);
        var instrumentResult = await instrumentRepo.CreateOneAsync(new Instrument() { Name = "Trombone" });
        context.ChangeTracker.Clear();
        Assert.True(instrumentResult?.Name == "Trombone");
        instrumentResult = await instrumentRepo.CreateOneAsync(new Instrument() { Name = "Guitar" });
        context.ChangeTracker.Clear();
        Assert.True(instrumentResult is null);
        var instrumentTotal = await instrumentRepo.GetAllAsync(null);
        Assert.True(instrumentTotal.Count() == 9);

        // genre

        GenreRepo genreRepo = new(context);
        var genreResult = await genreRepo.CreateOneAsync(new Genre() { Name = "Jazz" });
        context.ChangeTracker.Clear();
        Assert.True(genreResult?.Name == "Jazz");
        genreResult = await genreRepo.CreateOneAsync(new Genre() { Name = "Metal" });
        context.ChangeTracker.Clear();
        Assert.True(genreResult is null);
        var genreTotal = await genreRepo.GetAllAsync(null);
        Assert.True(genreTotal.Count() == 6);
    }
    [Fact]
    public async void GetAllBaseRepos()
    {
        using var context = Fixture.CreateContext();
        context.Database.BeginTransaction();

        CityRepo cityRepo = new(context);
        var result = await cityRepo.GetAllAsync(new BaseQueryOptions() { Limit = 1 });
        Assert.True(result.Count() == 1);
        result = await cityRepo.GetAllAsync(new NameFilter() { Name = "Espoo" });
        Assert.True(!result.Any());
        result = await cityRepo.GetAllAsync(new NameFilter() { Name = "Tampere" });
        Assert.True(result.First().Name == "Tampere");
        result = await cityRepo.GetAllAsync(new NameFilter() { Name = "hsfdpoer" });
        Assert.True(!result.Any());
    }
}