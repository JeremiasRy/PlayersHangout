namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class UserReadDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get => $"{FirstName} {LastName}"; }
    public string? City { get; set; }
    public ICollection<UserInstrument>? Instruments { get; set; }
}
