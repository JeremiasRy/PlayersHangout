using Backend.Src.Models;

namespace Backend.Src.DTOs;

public class UpdateUserDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string Email { get; set; } = null!;  
    public string NewPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Location Location { get; set; } = null!;
}
