namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.Repositories;
using Backend.Src.Models;

public abstract class BaseService<T,TReadDTO, TCreateDTO, TUpdateDTO> : IBaseService<T,TReadDTO, TCreateDTO, TUpdateDTO>
    where T : BaseModel, new()    
{
    protected readonly IBaseRepo<T> _repo;
    protected readonly IConverter<T, TReadDTO, TCreateDTO, TUpdateDTO> _converter;

    public BaseService(IBaseRepo<T> repo, IConverter<T, TReadDTO, TCreateDTO, TUpdateDTO> converter)
    {
        _repo = repo;
        _converter = converter;
    }

    public virtual async Task<TReadDTO> CreateAsync(TCreateDTO request)
    {
        var item = new T();
        _converter.CreateModel(item, request);
        var result = await _repo.CreateOneAsync(item);
        if (result is null)
        {
            throw new Exception();
        }   
        return _converter.ConvertReadDTO(item);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repo.DeleteOneAsync(id);        
    }

    public virtual async Task<ICollection<TReadDTO>> GetAllAsync(IFilterOptions? filter)
    {
        var items = await _repo.GetAllAsync(filter);        
        return items.Select(i => _converter.ConvertReadDTO(i)).ToList();
    }

    public async Task<TReadDTO> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
        {
           throw new ArgumentException("Did not find item with id");
        }
        return _converter.ConvertReadDTO(entity);
    }

    public async Task<TReadDTO> UpdateAsync(Guid id, TUpdateDTO request)
    {
        var entity = await _repo.GetByIdAsync(id);
        if(entity is null)
        {
            throw new ArgumentException("Did not find item with id");
        }
        _converter.UpdateModel(entity, request);
        var item = await _repo.UpdateOneAsync(entity);
        return _converter.ConvertReadDTO(item);
    }
}
