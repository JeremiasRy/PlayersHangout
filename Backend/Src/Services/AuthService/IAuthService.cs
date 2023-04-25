namespace Backend.Src.Services;

using Backend.Src.DTOs;

public interface IAuthService
{
    Task<AuthReadDTO?> Login(AuthSignInDTO request);
    Task<AuthReadDTO> SignUp(AuthSignUpDTO request);
    Task<bool> Logout();
}