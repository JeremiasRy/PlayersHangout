namespace Backend.src.Services.BaseService;

public interface IBaseService<T,TReadDTO, TCreateDto, TUpdateDto>
{    
    Task<IEnumerable<TReadDTO>> GetAllAsync(int page, int pageSize);
    Task<TReadDTO> CreateAsync (TCreateDto create);
    Task<TReadDTO> GetByIdAsync(Guid id);
    Task<TReadDTO> UpdateAsync(Guid id, TUpdateDto update);
    Task<bool> DeleteAsync(Guid id);
}