namespace Backend.Src.Converter.Location;

using Backend.Src.DTOs.Location;
using Backend.Src.Models;

public interface ILocationConverter : IConverter<Location, LocationReadDTO, LocationCreateDTO, LocationCreateDTO>
{
}
