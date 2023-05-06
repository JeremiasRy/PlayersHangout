namespace Backend.Src.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class User : IdentityUser<Guid>
{
    [MaxLength(60)]
    public string FirstName { get; set; } = null!;
    [MaxLength(60)]
    public string LastName { get; set; } = null!;
    public Location? Location { get; set; }
    public Guid? LocationId { get; set; }
    public ICollection<Wanted> Wanteds { get; set; } = null!;
    public ICollection<UserInstrument> Instruments { get; set; } = null!;
    public ICollection<Genre>? Genres { get; set; }
    public Instrument? MainInstrument { get; set; }
    public bool ActiveSession { get; set; } = false;
}
