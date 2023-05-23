namespace Backend.Src.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public Instrument MainInstrument() => Instruments.Single(instrument => instrument.IsMain).Instrument;
    public ICollection<Genre> Genres { get; set; } = null!;
    public bool ActiveSession { get; set; } = false;
}
