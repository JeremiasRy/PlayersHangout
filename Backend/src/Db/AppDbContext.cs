namespace Backend.src.Db;

using Backend.src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private readonly IConfiguration _config;

    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Instrument.SkillLevel>();
        // Not use time zone in EF.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder
            .UseNpgsql(configString)        
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);    
    }

    public DbSet<Instrument> Instruments { get; set; } = null!;
}