using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public interface IUserService
{
    Task<ICollection<UserReadDTO>> GetAllUsersAsync(IFilterOptions? filter); // We put some filters here also
    Task<User?> SignUpAsync(UserCreateDTO request);
    Task<TokenDTO> SignInAsync(SignInDTO request);
    Task<User?> UpdateUserAsync(Guid id, UserUpdateDTO request);
}
