using Backend.Src.Db.TestFixtures;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests;

[CollectionDefinition("TransactionalTests")]
public class TransactionalTestCollection : ICollectionFixture<TransactionalDbTestFixture>
{
}
[Collection("TransactionalTests")]
public class TransactionalRepoTests : IDisposable
{
    public TransactionalRepoTests(TransactionalDbTestFixture fixture)
    {
        Fixture = fixture;
    }
    public TransactionalDbTestFixture Fixture { get; set; }
    public void Dispose()
    {
        Fixture.Cleanup();
    }
    [Fact]
    public async void BaseRepoUpdate()
    {
        using var context = Fixture.CreateContext();

        InstrumentRepo instrumentRepo = new(context);
        var instruments = await instrumentRepo.GetAllAsync(null);
        var instrumentToUpdate = instruments.First();
        var originalName = instrumentToUpdate.Name;
        instrumentToUpdate.Name = "Test";
        var result = await instrumentRepo.UpdateOneAsync(instrumentToUpdate);
        Assert.True(result.Name == "Test");
        var result2 = await instrumentRepo.GetAllAsync(new NameFilter() { Name = originalName});
        Assert.True(!result2.Any());
    }
    [Fact]
    public async void BaseRepoDelete()
    {
        using var context = Fixture.CreateContext();
        int originalLength = context.Instruments.Count();
        InstrumentRepo instrumentRepo = new(context);
        var instruments = await instrumentRepo.GetAllAsync(null);
        var instrumentToDelete = instruments.First();
        var result = await instrumentRepo.DeleteOneAsync(instrumentToDelete.Id);
        Assert.True(result);
        Assert.True(originalLength - context.Instruments.Count() == 1);
        var result2 = await instrumentRepo.DeleteOneAsync(instrumentToDelete.Id);
        Assert.False(result2);
        var result3 = await instrumentRepo.GetAllAsync(new NameFilter() { Name = instrumentToDelete.Name });
        Assert.True(!result3.Any());
    }
}
