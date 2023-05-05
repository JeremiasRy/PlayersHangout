using Backend.Src.Db;
using Backend.Src.Models;

namespace Backend.Src.Repositories;

public class InstrumentRepo : BaseRepoName<Instrument>, IInstrumentRepo
{
    public InstrumentRepo(AppDbContext context) : base(context)
    {        

    }
}
