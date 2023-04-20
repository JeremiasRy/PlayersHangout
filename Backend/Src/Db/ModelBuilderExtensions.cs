using Backend.Src.Models;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Genres)
            .WithOne();

        modelBuilder.Entity<User>()
            .HasOne(user => user.MainInstrument)
            .WithMany();

        // modelBuilder.Entity<User>()
        //     .HasMany(user => user.Instruments)
        //     .WithOne();
            // .HasForeignKey(userInstrument => userInstrument.InstrumentId);

        modelBuilder.Entity<User>()
            .Navigation(user => user.Instruments)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(user => user.MainInstrument)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(user => user.Location)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(user => user.Genres)
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

    public static void AddUserInstrumentConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInstrument>()
            .HasKey(userInstrument => new { userInstrument.InstrumentId, userInstrument.UserId });
    }

    public static void AddGenresConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasIndex(genre => genre.Name)
            .IsUnique();                     
    }
}
