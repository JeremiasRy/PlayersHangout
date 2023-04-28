namespace Backend.Src.Converters;

using Backend.Src.Models;
using Backend.Src.DTOs;

public class WantedConverter : IWantedConverter
{
    public WantedReadDTO ConvertReadDTO(Wanted model)
    {
        return new WantedReadDTO
        {
            Description = model.Description,
            City = model.User.Location is not null ? model.User.Location.City.Name : "",
            Instrument = model.Instrument.Name,
            SkillLevel = model.SkillLevel,
        };
    }

    public void CreateModel(WantedCreateDTO create, out Wanted model)
    {
        model = new Wanted()
        {
            SkillLevel = create.SkillLevel,
            User = create.User,
            Description = create.Description,
            Genres = create.Genres
        };
    }

    public void UpdateModel(Wanted model, WantedUpdateDTO update)
    {
        model.Description = update.Description;
        model.Fullfilled = update.Fullfilled;
    }
}