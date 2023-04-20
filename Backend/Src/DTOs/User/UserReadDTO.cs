using Backend.Src.Models;

namespace Backend.Src.DTOs;

public class UserReadDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string FullName { get => $"{FirstName} ${LastName}"; }
    public string? City { get; set; }
    public ICollection<UserInstrument> Instruments { get; set; } = null!;
    public Instrument MainInstrument { get; set; } = null!;
    public static UserReadDTO FromUser(User user)
    {
        return new UserReadDTO 
        { 
            FirstName = user.FirstName, 
            LastName = user.LastName, 
            Instruments = user.Instruments, 
            MainInstrument = user.MainInstrument,
            City = user.Location.City
        };
    }
}
