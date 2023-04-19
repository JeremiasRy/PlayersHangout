namespace Backend.Src.DTOs;

using Backend.Src.Models;

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
