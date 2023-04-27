namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class UserUpdateDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string Email { get; set; } = null!;  
    public string NewPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public ICollection<Genre>? Genres { get; set; }
    public ICollection<UserInstrument>? Instruments { get; set; }

}
