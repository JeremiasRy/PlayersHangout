namespace Backend.Src.Services.WantedService;

using Backend.Src.Converter.Wanted;
using Backend.Src.DTOs;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Repositories.WantedRepo;
using Backend.Src.Models;
using Backend.Src.Services.Implementation;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    private readonly IUserService _userService;
    
    public WantedService(IUserService userService, IWantedRepo repo, IWantedConverter converter) : base(repo, converter)
    {
        _userService = userService;
    }
    public async Task<ICollection<UserReadDTO>> MatchUsersToWanted(MatchDTO match)
    {
        return await _userService.GetAllUsersAsync(match);
    }
}