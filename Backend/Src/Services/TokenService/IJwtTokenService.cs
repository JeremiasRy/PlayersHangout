namespace Backend.Src.Services;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IJwtTokenService
{
    Task<AuthReadDTO> GenerateToken(User user);
    Guid ReadUserIdFromToken(string token);
}
