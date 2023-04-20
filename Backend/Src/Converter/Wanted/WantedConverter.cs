namespace Backend.Src.Converter.Wanted;

using Backend.Src.Models;
using Backend.Src.DTOs.Wanted;

public class WantedConverter : IWantedConverter
{
    public WantedReadDTO ConvertReadDTO(Wanted model)
    {
        return new WantedReadDTO
        {
            Description = model.Description
        };
    }

    public void CreateModel(Wanted model, WantedCreateDTO create)
    {
        model.SkillLevel = create.SkillLevel;
        model.User = create.User;
        model.Description = create.Description;
        model.Genres = create.Genres;
    }

    public void UpdateModel(Wanted model, WantedUpdateDTO update)
    {
        model.Description = update.Description;
    }
}