namespace Backend.Src.Services;

public interface ICrudService<TModel, TDto> where TModel : new()
{
    Task<ICollection<TModel>> GetAllAsync();
    Task<TModel> GetByIdAsync(Guid id);
    Task<TModel> CreateAsync(TDto request);
    Task<TModel> UpdateAsync(Guid id, TDto request);
    Task<TModel> DeleteAsync(Guid id);
}
