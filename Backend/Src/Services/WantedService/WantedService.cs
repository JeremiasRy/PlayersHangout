namespace Backend.src.Services.WantedService;

using Backend.src.Converter.Wanted;
using Backend.src.DTOs;
using Backend.src.DTOs.Wanted;
using Backend.src.Repositories.WantedRepo;
using Backend.Src.Models;
using Backend.Src.Services.Implementation;


public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    private readonly IWantedRepo _repo;
    private readonly IWantedConverter _converter;

    public WantedService(IWantedRepo repo, IWantedConverter converter) : base(repo, converter)
    {
        _repo = repo;
        _converter = converter;
    }
}