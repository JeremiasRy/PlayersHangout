using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public interface IUserService
{
    Task<ICollection<UserReadDTO>> GetAllUsersAsync(IFilterOptions? filter); // We put some filters here also
    Task<User?> UpdateUserAsync(Guid id, UserUpdateDTO request);
    Task<UserReadDTO> GetUserProfile(string userId);
    Task<UserReadDTO> AddGenre(Guid userId, GenreDTO genre);
}
