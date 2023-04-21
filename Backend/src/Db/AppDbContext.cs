namespace Backend.Src.Db;

using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IConfiguration _configuration;
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<UserInstrument.SkillLevel>();
        // Not use time zone in EF.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration confirguration) : base(options) => _configuration = confirguration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder
            .UseNpgsql(connectionString)
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum<UserInstrument.SkillLevel>();
        modelBuilder.AddUserConfig();
        modelBuilder.AddUserInstrumentConfig();
        modelBuilder.AddTimestampConfig();
        modelBuilder.AddGenresConfig(); 
    }
    public DbSet<Instrument> Instruments { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Wanted> Wanteds { get; set; } = null!;
}