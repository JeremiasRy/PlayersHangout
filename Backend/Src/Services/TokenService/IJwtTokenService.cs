using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public interface IJwtTokenService
{
    Task<AuthReadDTO> GenerateToken(User user);
    string ReadUserIdFromToken(string token);
}
