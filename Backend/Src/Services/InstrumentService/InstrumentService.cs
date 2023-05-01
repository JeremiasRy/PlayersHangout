namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class InstrumentService : BaseService<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>
{
    public InstrumentService(IBaseRepo<Instrument> repo, IInstrumentConverter converter) : base(repo, converter)
    {
    }
}
