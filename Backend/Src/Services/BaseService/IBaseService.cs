namespace Backend.src.Services.BaseService;

public interface IBaseService<T, TCreateDto, TUpdateDto>
{    
    Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
    Task<T> CreateAsync (TCreateDto create);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> UpdateAsync(Guid id, TUpdateDto update);
    Task<bool> DeleteAsync(Guid id);
}