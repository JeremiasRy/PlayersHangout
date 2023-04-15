namespace Backend.Src.Services.Implementation;

using Backend.src.Services.BaseService;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

public class BaseService<T, TCreateDto, TUpdateDto> : IBaseService<T, TCreateDto, TUpdateDto> 
    where T : BaseModel, new()      
    where TCreateDto : BaseDTO<T>
    where TUpdateDto : BaseDTO<T>
{
    protected readonly AppDbContext _dbContext;
    public BaseService(AppDbContext dbContext) => _dbContext = dbContext; 
    
    public async Task<T> CreateAsync(TCreateDto request)
    {
        var item = new T();
        request.UpdateModel(item);
        _dbContext.Add(item);        
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = _dbContext.Set<T>().SingleOrDefault(model => model.Id == id);
        if (item != null)
        {
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;        
    }

    public async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize)
    {
        return await _dbContext
            .Set<T>()
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var item = await _dbContext
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(model => model.Id == id);

        return item is null ? throw new ArgumentException("Did not find item with id") : item;
    }

    public async Task<T> UpdateAsync(Guid id, TUpdateDto request)
    {
        var item = await GetByIdAsync(id);
        request.UpdateModel(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}
