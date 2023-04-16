namespace Backend.Src.Services.Implementation;

using Backend.src.Converter;
using Backend.src.Repositories.BaseRepo;
using Backend.src.Services.BaseService;
using Backend.Src.DTOs;
using Backend.Src.Models;

public class BaseService<T,TReadDTO, TCreateDTO, TUpdateDTO> : IBaseService<T,TReadDTO, TCreateDTO, TUpdateDTO>
    where T : BaseModel, new()    
    where TCreateDTO : BaseDTO<T>
    where TUpdateDTO : BaseDTO<T>
{
    protected readonly IBaseRepo<T> _repo;
    protected readonly IConvertReadDTO<T, TReadDTO> _converter;

    public BaseService(IBaseRepo<T> repo, IConvertReadDTO<T, TReadDTO> converter)
    {
        _repo = repo;
        _converter = converter;
    }

    public virtual async Task<TReadDTO> CreateAsync(TCreateDTO request)
    {
        var item = new T();
        request.UpdateModel(item);
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

    public virtual async Task<IEnumerable<TReadDTO>> GetAllAsync(IFilterOptions? filter)
    {
        var items = await _repo.GetAllAsync(filter);        
        return items.Select(i => _converter.ConvertReadDTO(i)).ToArray();
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
        request.UpdateModel(entity);
        var item = await _repo.UpdateOneAsync(entity);
        return _converter.ConvertReadDTO(item);
    }
}
