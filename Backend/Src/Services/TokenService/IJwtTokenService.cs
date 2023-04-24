namespace Backend.Src.Services;

using Backend.Src.DTOs.Auth;
using Backend.Src.Models;

public interface IJwtTokenService
{
    Task<AuthReadDTO> GenerateToken(User user);
}
