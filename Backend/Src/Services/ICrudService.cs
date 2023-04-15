namespace Backend.Src.Services;

public interface ICrudService<TModel, TDto>
{
    Task<ICollection<TModel>> GetAllAsync(int page, int pageSize);
    Task<TModel> GetByIdAsync(Guid id);
    Task<TModel> CreateAsync(TDto request);
    Task<TModel> UpdateAsync(Guid id, TDto request);
    Task<TModel> DeleteAsync(Guid id);
}