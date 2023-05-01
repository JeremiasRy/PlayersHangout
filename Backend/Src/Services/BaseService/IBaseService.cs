namespace Backend.Src.Services;

using Backend.Src.Repositories;
public interface IBaseService<T, TReadDTO, TCreateDto, TUpdateDto>
{
    Task<ICollection<TReadDTO>> GetAllAsync(IFilterOptions? request);
    Task<TReadDTO> CreateAsync(TCreateDto create);
    Task<TReadDTO> GetByIdAsync(Guid id);
    Task<TReadDTO> UpdateAsync(Guid id, TUpdateDto update);
    Task<bool> DeleteAsync(Guid id);
}