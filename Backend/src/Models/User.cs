namespace Backend.src.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{     
    [MaxLength(60)]
    public string FirstName { get; set; } = null!;
    [MaxLength(60)]
    public string? LastName { get; set; }
    public ICollection<Instrument> Instruments { get; set; } = null!;
    public Instrument MainInstrument { get; set; } = null!;
}