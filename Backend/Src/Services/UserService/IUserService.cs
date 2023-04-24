namespace Backend.Src.Services.UserService;

using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IUserService
{
    Task<ICollection<UserReadDTO>> GetAllUsersAsync(IFilterOptions? filter); // We put some filters here also
    Task<User?> UpdateUserAsync(Guid id, UserUpdateDTO request);
}
