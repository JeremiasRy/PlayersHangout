using Backend.Src.Models;

namespace Backend.Src.DTOs;

public class UserDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string FullName { get => $"{FirstName} ${LastName}"; }
    public string? City { get; set; }
    public ICollection<UserInstrument> Instruments { get; set; } = null!;
    public Instrument MainInstrument { get; set; } = null!;
    public static UserDTO FromUser(User user)
    {
        return new UserDTO 
        { 
            FirstName = user.FirstName, 
            LastName = user.LastName, 
            Instruments = user.Instruments, 
            MainInstrument = user.MainInstrument,
            City = user.Location.City
        };
    }
}
