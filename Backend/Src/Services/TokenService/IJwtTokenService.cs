using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public interface IJwtTokenService
{
    Task<TokenDTO> GenerateToken(User user);
}
