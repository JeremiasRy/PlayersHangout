namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{
    public WantedService(IBaseRepo<Wanted> repo, IWantedConverter converter) : base(repo, converter)
    {
    }
}