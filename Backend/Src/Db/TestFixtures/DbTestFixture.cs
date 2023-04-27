namespace Backend.Src.Db.TestFixtures;

using Microsoft.EntityFrameworkCore;
using Backend.Src.Db;
using Backend.Src.Models;

public class DbTestFixture
{
    private IConfiguration _configuration;
    private static readonly object _lock = new();
    private static bool _dbInitialized;
    public DbTestFixture(IConfiguration configuration)
    {
        _configuration = configuration;
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


                }
            }
        }
    }
    public AppDbContext CreateContext() => new AppDbContext(_configuration, DbType.Test);
}
