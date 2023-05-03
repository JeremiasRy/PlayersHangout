namespace BackendTests;

using Backend.Src.Db.TestFixtures;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

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
    public async void GetAllBaseRepos()
    {
        using var context = Fixture.CreateContext();
        context.Database.BeginTransaction();

        await TestRun<Genre, GenreRepo>(new GenreRepo(context));
        await TestRun<Instrument, InstrumentRepo>(new InstrumentRepo(context));
        await TestRun<City, CityRepo>(new CityRepo(context));

        static async Task TestRun<TModel, TRepo> (TRepo repo) where TModel : HasName, new() where TRepo : BaseRepoName<TModel>
        {
            IEnumerable<TModel> result = await repo.GetAllAsync(null);

            for (int i = 0; i < result.Count(); i++)
            {
                IEnumerable<TModel> searchResult = await repo.GetAllAsync(new NameFilter() { Name = result.ElementAt(i).Name });
                Assert.Equal(result.ElementAt(i).Name, searchResult.First().Name);
            }
            int originalLength = result.Count();
            result = await repo.GetAllAsync(new BaseQueryOptions() { Limit = 1});
            Assert.True(result.Count() == 1);

            result = await repo.GetAllAsync(new BaseQueryOptions() { Limit = originalLength - 1 });
            Assert.Equal(originalLength - 1, result.Count());

            result = await repo.GetAllAsync(null);
            TModel secondResult = result.ElementAt(1);
            result = await repo.GetAllAsync(new BaseQueryOptions() { Limit = 1, Skip = 1});
            Assert.Equal(secondResult.Name, result.First().Name);
            Assert.Equal(secondResult.Id, result.First().Id);
        }
    }
    [Fact]
    public async void GetByIdBaseRepos()
    {
        using var context = Fixture.CreateContext();
        context.Database.BeginTransaction();

        await TestRun<Genre, GenreRepo>(new GenreRepo(context));
        await TestRun<Instrument, InstrumentRepo>(new InstrumentRepo(context));
        await TestRun<City, CityRepo>(new CityRepo(context));

        async static Task TestRun<TModel, TRepo>(TRepo repo) where TModel : HasName, new() where TRepo : BaseRepo<TModel> 
        {
            IEnumerable<TModel> allItems = await repo.GetAllAsync(null);
            for (int i = 0; i < allItems.Count(); i++)
            {
                TModel? item = await repo.GetByIdAsync(allItems.ElementAt(i).Id);
                Assert.NotNull(item);
                foreach (var property in typeof(Genre).GetProperties())
                {
                    Assert.Equal(property.GetValue(allItems.ElementAt(i)), property.GetValue(item));
                }
            }
        }
    }
    [Fact]
    public async void CreateBaseRepos()
    {
        using var context = Fixture.CreateContext();
        context.Database.BeginTransaction();

        await TestRun<Genre, GenreRepo> (new GenreRepo(context));
        context.ChangeTracker.Clear();
        await TestRun<Instrument, InstrumentRepo> (new InstrumentRepo(context));
        context.ChangeTracker.Clear();
        await TestRun<City, CityRepo>(new CityRepo(context));

        static async Task TestRun<TModel, TRepo>(TRepo repo) where TModel : HasName, new() where TRepo : BaseRepoName<TModel>
        {
            TModel? createItem = await repo.CreateOneAsync(new TModel() { Name = "Test" });
            Assert.NotNull(createItem);
            try
            {
                await repo.CreateOneAsync(new TModel() { Name = "Test" });
                Assert.Fail("Repo should throw exception for unique name");
            } catch
            {
                Assert.True(true);
            }

            TModel? result = await repo.GetByIdAsync(createItem.Id);
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }
    }
}
