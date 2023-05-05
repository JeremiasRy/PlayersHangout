using Backend.Src.Models;
using Backend.Src.Db;
using Backend.Src.Converters;
using Backend.Src.Repositories;
using Backend.Src.DTOs;

namespace Backend.Src.Services;

public class BaseServiceName<T, TReadDTO, TCreateDTO, TUpdateDTO> : BaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> where T : HasName, new()
{
    public BaseServiceName(IBaseRepoName<T> repo, IConverter<T, TReadDTO, TCreateDTO, TUpdateDTO> converter) : base(repo, converter)
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
        return _converter.ConvertReadDTO(await _repo.CreateOneAsync(item));
    }
}
