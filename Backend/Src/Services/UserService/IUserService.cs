using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public interface IUserService
{
    Task<ICollection<UserDTO>> GetAllUsersAsync(int page, int pageSize); // We put some filters here also
    Task<User?> SignUpAsync(SignUpDTO request);
    Task<SignInResponseDTO> SignInAsync(SignInDTO request);
    Task<User?> UpdateUserAsync(Guid id, UpdateUserDTO request);
}
