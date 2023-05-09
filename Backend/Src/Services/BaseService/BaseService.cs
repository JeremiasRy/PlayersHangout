using Backend.Src.Converter;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;
public abstract class BaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> : IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO>
    where T : BaseModel, new()
    where TReadDTO : new()
{
    protected readonly IBaseRepo<T> _repo;
    protected readonly IConverter _converter;

    public BaseService(IBaseRepo<T> repo, IConverter converter)
    {
        _repo = repo;
        _converter = converter;
    }

    public virtual async Task<TReadDTO?> CreateAsync(TCreateDTO request)
    {
        _converter.CreateModel(request, out T item);
        var result = await _repo.CreateOneAsync(item);
        return _converter.ConvertReadDTO(result, default(TReadDTO));
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _repo.GetByIdAsync(id);
        if (result is null)
        {
            return false;
        }
        await _repo.DeleteOneAsync(result);
        return true;
    }

    public virtual async Task<ICollection<TReadDTO>> GetAllAsync(IFilterOptions? filter)
    {
        var items = await _repo.GetAllAsync(filter);
        return items.Select(i => _converter.ConvertReadDTO(i, default(TReadDTO))).ToList();
    }

    public async Task<TReadDTO?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? default : _converter.ConvertReadDTO(entity, default(TReadDTO));
    }

    public async Task<TReadDTO> UpdateAsync(Guid id, TUpdateDTO request)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
        {
            throw new ArgumentException("Did not find item with id");
        }
        _converter.UpdateModel(entity, request);
        var item = await _repo.UpdateOneAsync(entity);
        return _converter.ConvertReadDTO(item, default(TReadDTO));
    }
}
