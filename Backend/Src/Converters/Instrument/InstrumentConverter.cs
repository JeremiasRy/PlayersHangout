namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public class InstrumentConverter : IInstrumentConverter
{
    public InstrumentDTO ConvertReadDTO(Instrument model)
    {
        return new InstrumentDTO()
        {
            Name = model.Name,
        };
    }

    public void CreateModel(InstrumentDTO create, out Instrument model)
    {
        model = new Instrument()
        {
            Name = create.Name
        };
    }

    public void UpdateModel(Instrument model, InstrumentDTO update)
    {
        model.Name = update.Name;
    }
}
