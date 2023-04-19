namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class SignInResponseDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<UserInstrument> Instruments { get; set; } = null!;
    public Instrument MainInstrument { get; set; } = null!;
    public string[] Roles { get; set; } = null!;
    public string Token { get; set; } = null!;

    public static SignInResponseDTO FromUser(User user, string token, string[] roles)
    {
        return new SignInResponseDTO
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
