namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class InstrumentService : BaseServiceName<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>
{
    public InstrumentService(BaseRepoName<Instrument> repo, IInstrumentConverter converter) : base(repo, converter)
    {
    }
}
