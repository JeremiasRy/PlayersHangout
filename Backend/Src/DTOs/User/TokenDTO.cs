using Backend.Src.Models;

namespace Backend.Src.DTOs;

public class TokenDTO
{
    public string[] Roles { get; set; } = null!;
    public string Token { get; set; } = null!;

    public static TokenDTO FromUser(User user, string token, string[] roles)
    {
        return new TokenDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Instruments = user.Instruments,
            MainInstrument = user.MainInstrument,
            Token = token,
            Roles = roles
        };
    }

}
