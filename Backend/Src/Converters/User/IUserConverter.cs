namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IUserConverter : IConverter<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
}
