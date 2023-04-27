using Backend.Src.Models;
using Backend.Src.DTOs;

namespace Backend.Src.Converters;

public interface IUserInstrumentConverter : IConverter<UserInstrument, UserInstrumentReadDTO, UserInstrumentCreateDTO, UserInstrumentUpdateDTO>
{
}
