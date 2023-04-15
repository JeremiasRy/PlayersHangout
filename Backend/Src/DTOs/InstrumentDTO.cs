using Backend.Src.Models;
using static Backend.Src.Models.Instrument;

namespace Backend.Src.DTOs;

public class InstrumentDTO : BaseDTO<Instrument>
{
    public string Name { get; set; } = null!;
    public override void UpdateModel(Instrument model)
    {
        model.Name = Name;
    }
}
