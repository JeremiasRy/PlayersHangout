using Backend.Src.Models;
using Backend.Src.Repositories.BaseRepo;
using Backend.Src.Models;
using Backend.Src.Repositories.BaseRepo;

public class MatchDTO : IFilterOptions // works both ways wanted -> user || user -> wanted
{
    public string City { get; set; } = null!;
    public ICollection<Instrument>? Instruments { get; set; }
    public ICollection<Genre>? Genres { get; set; }
}
