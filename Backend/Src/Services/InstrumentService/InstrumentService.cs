namespace Backend.Src.Services;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class InstrumentService : BaseServiceName<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>, IInstrumentService
{
    public InstrumentService(BaseRepoName<Instrument> repo, IConverter converter) : base(repo, converter)
    {
    }
}
