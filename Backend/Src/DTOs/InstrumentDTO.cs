using Backend.Src.Models;

namespace Backend.Src.DTOs;

public class InstrumentDTO : BaseDTO<Instrument>
{
    public string Name { get; set; } = null!;
    public override void UpdateModel(Instrument model)
    {
        model.Name = Name;
    }
}
