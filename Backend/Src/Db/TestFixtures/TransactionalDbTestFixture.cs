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
            new Genre() { Name = "Metal", NameNormalized = "METAL" },
            new Genre() { Name = "Rock", NameNormalized = "ROCK" },
            new Genre() { Name = "Pop", NameNormalized = "POP" },
            new Genre() { Name = "Punk", NameNormalized = "PUNK" },
            new Genre() { Name = "Death Metal", NameNormalized = "DEATH METAL" }
            );

        // Instruments
        context.AddRange(
            new Instrument() { Name = "Guitar", NameNormalized = "GUITAR" },
            new Instrument() { Name = "Bass", NameNormalized = "BASS" },
            new Instrument() { Name = "Drums", NameNormalized = "DRUMS" },
            new Instrument() { Name = "Vocals", NameNormalized = "VOCALS" },
            new Instrument() { Name = "Background Vocals", NameNormalized = "BACKGROUND VOCALS" },
            new Instrument() { Name = "Keyboard", NameNormalized = "KEYBOARD" },
            new Instrument() { Name = "Piano", NameNormalized = "PIANO" },
            new Instrument() { Name = "Saxophone", NameNormalized = "SAXOPHONE" }
            );

        // Cities
        context.AddRange(
            new City() { Name = "Helsinki", NameNormalized = "HELSINKI" },
            new City() { Name = "Tampere", NameNormalized = "TAMPERE" }
            );
        context.SaveChanges();
    }
    public AppDbContext CreateContext() => new(Configuration, DbType.Transactional);
}
