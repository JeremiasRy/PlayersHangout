using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace Backend.Src.Services;
public abstract class BaseService<T, TDto> : IBaseService<T, TDto>
    where T : BaseModel, new()
{
    protected readonly IConverter _converter;
    protected readonly AppDbContext _appDbContext;

    public BaseService(IConverter converter, AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _converter = converter;
    }

    public virtual async Task<T> CreateAsync(TDto create)
    {
        _converter.CreateModel(create, out T model);
        if (model is null)
        {
            throw new Exception("Couldn't create new model from request.");
        }
        _appDbContext.Add(model);
        await _appDbContext.SaveChangesAsync();
        return model;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var result = _appDbContext
            .Set<T>()
            .SingleOrDefault(item => item.Id == id) ?? throw new Exception("No item found with id");
        _appDbContext.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }

    public virtual async Task<ICollection<T>> GetAllAsync(IFilterOptions? request)
    {
        if (request is BaseQueryOptions queryOptions)
        {
            return await _appDbContext
                .Set<T>()
                .Skip(queryOptions.Skip)
                .Take(queryOptions.Limit)
                .ToListAsync();
        }
        return await _appDbContext
            .Set<T>()
            .Take(30)
            .ToListAsync();
    }

    public async virtual Task<T?> GetByIdAsync(Guid id)
    {
        return await _appDbContext
            .Set<T>()
            .SingleOrDefaultAsync(item => item.Id == id);
    }

    public async virtual Task<T> UpdateAsync(Guid id, TDto update)
    {
        var item = await GetByIdAsync(id) ?? throw new Exception("Can't find item with ID");
        _converter.UpdateModel(item, update);
        _appDbContext.Update(item);
        await _appDbContext.SaveChangesAsync();
        return item;
    }
}
