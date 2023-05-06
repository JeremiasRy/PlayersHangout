namespace Backend.Src.Services;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    public WantedService(IBaseRepo<Wanted> repo, IConverter converter) : base(repo, converter)
    {
    }
}