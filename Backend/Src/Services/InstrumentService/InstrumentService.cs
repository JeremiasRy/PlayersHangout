using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class InstrumentService : BaseServiceName<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>, IInstrumentService
{
    public InstrumentService(IInstrumentRepo repo, IConverter converter) : base((InstrumentRepo)repo, converter)
    {
    }
}
