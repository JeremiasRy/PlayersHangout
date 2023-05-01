using Backend.Src.Models;

namespace Backend.Src.Db.TestFixtures;

public class TransactionalDbTestFixture
{
    public readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    public TransactionalDbTestFixture()
    {
        using var context = CreateContext(); // Create db with some test data
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Cleanup();

    }
    public void Cleanup()
    {
        using var context = CreateContext();
        context.RemoveRange(context.Genres);
        context.RemoveRange(context.Instruments);
        context.RemoveRange(context.Cities);

        // Genres
        context.AddRange(
            new Genre() { Name = "Metal" },
            new Genre() { Name = "Rock" },
            new Genre() { Name = "Pop" },
            new Genre() { Name = "Punk" },
            new Genre() { Name = "Death Metal" }
            );

        // Instruments
        context.AddRange(
            new Instrument() { Name = "Guitar" },
            new Instrument() { Name = "Bass" },
            new Instrument() { Name = "Drums" },
            new Instrument() { Name = "Vocals" },
            new Instrument() { Name = "Background vocals" },
            new Instrument() { Name = "Keyboard" },
            new Instrument() { Name = "Piano" },
            new Instrument() { Name = "Saxophone" }
            );

        // Cities
        context.AddRange(
            new City() { Name = "Helsinki" },
            new City() { Name = "Tampere" }
            );
        context.SaveChanges();
    }
    public AppDbContext CreateContext() => new(Configuration, DbType.Transactional);
}
