using Backend.Src.Models;

namespace Backend.Src.DTOs;

public class InstrumentDTO : BaseDTO<Instrument>
{
    public string Name { get; set; } = null!;

    public override void UpdateModel(Instrument model)
    {
        model.Name = Name;
    }

    public static implicit operator InstrumentDTO(Instrument model)
    {
        return new InstrumentDTO
        {
            Name = model.Name
        };
    }
}
