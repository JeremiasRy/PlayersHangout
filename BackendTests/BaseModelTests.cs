namespace BackendTests;

using Microsoft.AspNetCore.Identity;
using Backend.Src.Controllers;
using Backend.Src.Converters;
using Backend.Src.Db.TestFixtures;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using Backend.Src.Services;
using Backend.Src.Models;
using System.Security.Claims;
using Moq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

public class BaseModelTests : IClassFixture<DbTestFixture>
{
    public BaseModelTests(DbTestFixture fixture)
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
    public async void CreateBaseModels()
    {
        IBaseService<City, City, City, City> baseService = new CityService(new CityRepo(Fixture.CreateContext()), new CityConverter());
        var result = await baseService.GetAllAsync(null);
        Assert.True(result.Count == 2);
        var shouldBeNull = await baseService.CreateAsync(new City() { Name = "Tampere" });
        Assert.True(shouldBeNull is null);
        shouldBeNull = await baseService.CreateAsync(new City() { Name = "Helsinki" });
        Assert.True(shouldBeNull is null);
    }
}