using Backend.Src.Db;
using Backend.Src.Models;
using Backend.Src.Repositories.BaseRepo;

namespace Backend.Src.Repositories.InstrumentRepo;

public class InstrumentRepo : BaseRepo<Instrument>, IInstrumentRepo
{
    public InstrumentRepo(AppDbContext context) : base(context)
    {
    }
}
