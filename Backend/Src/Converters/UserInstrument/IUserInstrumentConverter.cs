using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Converters;

public interface IUserInstrumentConverter : IConverter<UserInstrument, UserInstrumentReadDTO, UserInstrumentCreateDTO, UserInstrumentUpdateDTO>
{
}
