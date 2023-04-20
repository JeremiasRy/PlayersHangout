namespace Backend.Src.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    [MaxLength(60)]
    public string FirstName { get; set; } = null!;
    [MaxLength(60)]
    public string LastName { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public Guid LocationId { get; set; }
    public ICollection<Wanted> Wanteds { get; set; } = null!;
    public ICollection<UserInstrument> Instruments { get; set; } = null!;
    public ICollection<Genre>? Genres { get; set; }
    public Instrument MainInstrument { get; set; } = null!;
}