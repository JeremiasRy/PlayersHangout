namespace Backend.src.Converter.Wanted;

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
}