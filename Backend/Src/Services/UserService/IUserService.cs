namespace Backend.Src.Services;

using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IUserService
{
    Task<ICollection<UserDTO>> GetAllUsersAsync(IFilterOptions? filter); // We put some filters here also
    Task<User?> SignUpAsync(SignUpDTO request);
    Task<SignInResponseDTO> SignInAsync(SignInDTO request);
    Task<User?> UpdateUserAsync(Guid id, UpdateUserDTO request);
}
