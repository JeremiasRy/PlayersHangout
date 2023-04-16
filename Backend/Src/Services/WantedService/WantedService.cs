namespace Backend.src.Services.WantedService;

using Backend.src.Converter.Wanted;
using Backend.src.DTOs;
using Backend.src.DTOs.Wanted;
using Backend.src.Repositories.WantedRepo;
using Backend.Src.DTOs;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Models;
using Backend.Src.Services;
using Backend.Src.Services.Implementation;
using System;
using System.Threading.Tasks;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    private readonly IUserService _userService;
    public WantedService(IUserService userService, IWantedRepo repo, IWantedConverter converter) : base(repo, converter)
    {
        _userService = userService;
    }

    public async Task<ICollection<UserDTO>> MatchUsersToWanted(MatchDTO match)
    {
        return await _userService.GetAllUsersAsync(match);
    }
}