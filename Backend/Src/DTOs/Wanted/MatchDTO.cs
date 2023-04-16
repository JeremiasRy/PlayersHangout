using Backend.src.Models;
using Backend.src.Repositories.BaseRepo;
using Backend.Src.Models;

namespace Backend.Src.DTOs.Wanted;

public class MatchDTO : IFilterOptions // works both ways wanted -> user || user -> wanted
{
    public string City { get; set; } = null!;
    public ICollection<Instrument>? Instruments { get; set; }
    public ICollection<Genre>? Genres { get; set; }
}
