using Backend.Src.Models;
using Backend.Src.Db;
using Backend.Src.Converter;
using Backend.Src.Repositories;
using Backend.Src.DTOs;

namespace Backend.Src.Services;

public class BaseServiceName<T, TReadDTO, TCreateDTO, TUpdateDTO> : BaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> 
    where T : HasName, new()
    where TReadDTO : new()
{
    public BaseServiceName(BaseRepoName<T> repo, IConverter converter) : base(repo, converter)
    {
    }

    public async override Task<TReadDTO?> CreateAsync(TCreateDTO request)
    {
        _converter.CreateModel(request, out T item);
        var check = await _repo.GetAllAsync(new NameFilter() { Name = item.Name });
        if (check.Any())
        {
            return default;
        }
        return _converter.ConvertReadDTO<T, TReadDTO>(await _repo.CreateOneAsync(item));
    }
}
