namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IWantedConverter : IConverter<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{ }