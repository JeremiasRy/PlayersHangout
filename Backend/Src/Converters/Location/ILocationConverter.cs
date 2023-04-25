namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface ILocationConverter : IConverter<Location, LocationReadDTO, LocationCreateDTO, LocationCreateDTO>
{
}
