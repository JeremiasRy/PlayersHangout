namespace Backend.Src.Services.WantedService;

using Backend.Src.Converter.Wanted;
using Backend.Src.DTOs;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Repositories.WantedRepo;
using Backend.Src.Models;
using Backend.Src.Services.Implementation;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs.Filter;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    public WantedService(IUserService userService, IWantedRepo repo, IWantedConverter converter) : base(repo, converter)
    {
    }
}