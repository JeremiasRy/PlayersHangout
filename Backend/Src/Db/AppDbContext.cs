namespace Backend.Src.Db;

using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IConfiguration _configuration;
    private readonly DbType _dbType;
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<User.LevelOfCommitment>();
        // Not use time zone in EF.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public AppDbContext(IConfiguration configuration, DbType dbType = DbType.Production)
    {
        _dbType = dbType;
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(ConnectionString())
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
    }
    private string ConnectionString()
    {
        switch (_dbType)
        {
            case DbType.Production:
                {
                    return _configuration.GetConnectionString("DefaultConnection");
                }
            case DbType.Test:
                {
                    return _configuration.GetConnectionString("TestConnection");
                }
            case DbType.Transactional:
                {
                    return _configuration.GetConnectionString("TransactionalConnection");
                }
        }
        throw new Exception("No database type was provided");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum<User.LevelOfCommitment>();
        modelBuilder.AddUserConfig();
        modelBuilder.AddUserInstrumentConfig();
        modelBuilder.AddTimestampConfig();
        modelBuilder.AddGenresConfig();
        modelBuilder.AddCitiesConfig();

    }
    public DbSet<Instrument> Instruments { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Wanted> Wanteds { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
}
public enum DbType
{
    Production,
    Test,
    Transactional
}
