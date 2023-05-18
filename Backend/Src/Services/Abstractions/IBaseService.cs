namespace Backend.Src.Services;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IBaseService<T, TDto> 
    where T : BaseModel, new()
{
    Task<ICollection<T>> GetAllAsync(IFilterOptions? request);
    Task<T> CreateAsync(TDto create);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> UpdateAsync(Guid id, TDto update);
    Task DeleteAsync(Guid id);
}