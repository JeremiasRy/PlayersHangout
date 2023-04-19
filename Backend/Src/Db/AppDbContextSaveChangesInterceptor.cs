namespace Backend.Src.Db;

using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class AppDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    public void UpdateTimeStamps(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries()
            .Where(entity => entity.Entity is BaseModel && (entity.State == EntityState.Added || entity.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((BaseModel)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
            else
            {
                ((BaseModel)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateTimeStamps(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
