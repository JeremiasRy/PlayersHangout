using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public class InstrumentService : BaseServiceNameFilter<Instrument, InstrumentDTO>, IInstrumentService
{
    public InstrumentService(AppDbContext appDbContext, IConverter converter) : base(converter, appDbContext)
    {
    }
}
