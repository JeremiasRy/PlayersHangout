namespace Backend.Src.Converter.User;

using Backend.Src.DTOs;
using Backend.Src.Models;

public interface IUserConverter : IConverter<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
}
