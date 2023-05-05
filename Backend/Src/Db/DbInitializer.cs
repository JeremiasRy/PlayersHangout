using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Src.Db;

public class DbInitializer : IHostedService
{
    private readonly AppDbContext _context;

    public DbInitializer(AppDbContext context) => _context = context;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var passwordHasher = new PasswordHasher<User>();

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "Player Hangout",
            Email = "player-hangout@example.com",
            FirstName = "Player",
            LastName = "Hangout",            
        };

        newUser.PasswordHash = passwordHasher.HashPassword(newUser, "PlayerHangout123*");
        

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}