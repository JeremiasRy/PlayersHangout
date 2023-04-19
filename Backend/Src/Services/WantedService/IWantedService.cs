namespace Backend.Src.Services.WantedService;

using Backend.Src.DTOs.Wanted;
using Backend.Src.Services.BaseService;
using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IWantedService : IBaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{
    Task<ICollection<UserDTO>> MatchUsersToWanted(MatchDTO match);
}
