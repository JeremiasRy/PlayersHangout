using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Db;

public static class ModelBuilderExtensions
{
    public static void AddUserConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");

        modelBuilder.Entity<User>()
           .HasIndex(user => user.FirstName);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.LastName);

        modelBuilder.Entity<User>()
            .HasIndex(user => new { user.FirstName, user.LastName })
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasOne(user => user.Location)
            .WithOne();

        modelBuilder.Entity<User>()
            .HasMany(user => user.Wanteds)
            .WithOne();

        modelBuilder.Entity<User>()
            .HasOne(user => user.MainInstrument)
            .WithMany();

        modelBuilder.Entity<User>()
            .Navigation(user => user.Instruments)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(user => user.MainInstrument)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(user => user.Location)
            .AutoInclude();
    }
    public static void AddTimestampConfig(this ModelBuilder modelBuilder)
    {
        foreach (var propertyName in modelBuilder.Model.GetEntityTypes().Select(s => s.Name))
        {
            modelBuilder.Entity(propertyName)
                .Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity(propertyName)
                .Property<DateTime>("UpdatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
