using Backend.Src.DTOs;

namespace Backend.Src.Services;

public interface IAuthService
{
    Task<AuthReadDTO?> Login(AuthSignInDTO request);
    Task<AuthReadDTO> SignUp(AuthSignUpDTO request);
    Task<bool> Logout(string userId);
}