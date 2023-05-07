using Backend.Src.Db;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Repositories;

public class UserInstrumentRepo : IUserInstrumentRepo
{
    private readonly AppDbContext _context;
    public UserInstrumentRepo(AppDbContext context) => _context = context;

    public async Task<UserInstrument> CreateOneAsync(UserInstrument model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task DeleteOneAsync(UserInstrument model)
    {
        _context.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<UserInstrument> UpdateOneAsync(UserInstrument model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }
}
