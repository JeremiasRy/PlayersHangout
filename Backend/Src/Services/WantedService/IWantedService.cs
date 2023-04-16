namespace Backend.src.Services.WantedService;

using Backend.src.DTOs.Wanted;
using Backend.src.Services.BaseService;
using Backend.Src.DTOs;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Models;

public interface IWantedService : IBaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{
    Task<ICollection<UserDTO>> MatchUsersToWanted(MatchDTO match);
}
