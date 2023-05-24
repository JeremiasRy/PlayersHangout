namespace Backend.Src.Db.TestFixtures;

using Backend.Src.Db;
using Backend.Src.Models;

public class DbTestFixture
{
    private static readonly object _lock = new();
    private static bool _dbInitialized;

    public readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    public DbTestFixture()
    {
        // Thread safety
        lock (_lock)
        {
            if (!_dbInitialized)
            {
                using (var context = CreateContext()) // Create db with some test data
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

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
                _dbInitialized = true;
            }
        }
    }
    public AppDbContext CreateContext() => new(Configuration, DbType.Test);
}
