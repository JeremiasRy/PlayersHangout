namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class InstrumentRepo : BaseRepo<Instrument>
{
    public InstrumentRepo(AppDbContext context) : base(context)
    {
    }
}
