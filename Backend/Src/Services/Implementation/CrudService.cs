using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Services.Implementation;

public class CrudService<TModel, TDto> : ICrudService<TModel, TDto> 
    where TModel : BaseModel, new()  
    where TDto : BaseDTO<TModel>
{
    protected readonly AppDbContext _dbContext;
    public CrudService(AppDbContext dbContext) => _dbContext = dbContext; 
    public async Task<TModel> CreateAsync(TDto request)
    {
        var item = new TModel();
        request.UpdateModel(item);
        _dbContext.Add(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<TModel> DeleteAsync(Guid id)
    {
        var item = _dbContext.Set<TModel>().SingleOrDefault(model => model.Id == id);
        if (item != null)
        {
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
        throw new ArgumentException("Did not find item with id");
    }

    public async Task<ICollection<TModel>> GetAllAsync()
    {
        return await _dbContext
            .Set<TModel>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TModel> GetByIdAsync(Guid id)
    {
        var item = await _dbContext
            .Set<TModel>()
            .AsNoTracking()
            .SingleOrDefaultAsync(model => model.Id == id);

        return item is null ? throw new ArgumentException("Did not find item with id") : item;
    }

    public async Task<TModel> UpdateAsync(Guid id, TDto request)
    {
        var item = await GetByIdAsync(id);
        request.UpdateModel(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}
