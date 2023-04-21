using Backend.Src.Converter.Instrument;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories.InstrumentRepo;
using Backend.Src.Services.Implementation;

namespace Backend.Src.Services.InstrumentService;

public class InstrumentService : BaseService<Instrument, InstrumentDTO,  InstrumentDTO, InstrumentDTO>, IInstrumentService
{
    public InstrumentService(IInstrumentRepo repo, IInstrumentConverter converter) : base(repo, converter)
    {   
    }
}
