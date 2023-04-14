using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Db;

public static class ModelBuilderExtensions
{
    public static void AddUserConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("role_claims");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_roles");

        modelBuilder.Entity<User>()
           .HasIndex(user => user.FirstName);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.LastName);

        modelBuilder.Entity<User>()
            .HasIndex(user => new { user.FirstName, user.LastName })
            .IsUnique();

        modelBuilder.Entity<User>()
            .Navigation(user => user.Instruments)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .HasOne(user => user.MainInstrument)
            .WithMany();
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
