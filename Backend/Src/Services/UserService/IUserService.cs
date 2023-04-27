namespace Backend.Src.Services;

using Backend.Src.Repositories;
using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IUserService
{
    Task<ICollection<UserReadDTO>> GetAllUsersAsync(IFilterOptions? filter); // We put some filters here also
    Task<User?> UpdateUserAsync(Guid id, UserUpdateDTO request);
    Task<UserReadDTO> GetUserProfile();
    Task<UserReadDTO> AddInstrument(Guid userId, UserInstrumentCreateDTO request);
    Task<UserReadDTO> AddGenre(Guid userId, string genre);

}
