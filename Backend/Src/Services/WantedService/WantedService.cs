namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using Backend.Src.Models;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{
    public WantedService(IBaseRepo<Wanted> repo, IWantedConverter converter) : base(repo, converter)
    {
    }
}