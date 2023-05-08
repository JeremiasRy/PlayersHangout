using Backend.Src.Models;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using Backend.Src.Converter;

namespace Backend.Src.Services;
public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    public WantedService(IWantedRepo repo, IConverter converter) : base(repo, converter)
    {
    }
}