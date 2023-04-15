using System.Globalization;

namespace Backend.Src.DTOs;

public class SignInDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
