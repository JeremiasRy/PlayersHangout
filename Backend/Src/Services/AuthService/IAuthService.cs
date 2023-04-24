namespace Backend.Src.Services.AuthService;

using Backend.Src.DTOs.Auth;

public interface IAuthService
{
    Task<AuthReadDTO?> Login(AuthSignInDTO request);
    Task<AuthReadDTO> SignUp(AuthSignUpDTO request);
    Task<bool> Logout();
}