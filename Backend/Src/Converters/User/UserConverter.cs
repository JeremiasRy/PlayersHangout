namespace Backend.Src.Converters;

using Backend.Src.DTOs;
using Backend.Src.Models;

public class UserConverter : IUserConverter
{
    private readonly IUserInstrumentConverter _userInstrumentConverter;
    public UserConverter(IUserInstrumentConverter userInstrumentConverter) => _userInstrumentConverter = userInstrumentConverter;
    public UserReadDTO ConvertReadDTO(User model)
    {
        return new UserReadDTO
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Instruments = model.Instruments.Select(instrument => _userInstrumentConverter.ConvertReadDTO(instrument)).ToList(),
            MainInstrument = model.MainInstrument ?? null,
            City = model.Location is not null ? model.Location.City.Name : "",
        };
    }

    public void CreateModel(UserCreateDTO create, out User model)
    {
        model = new User()
        {
            FirstName = create.FirstName,
            LastName = create.LastName,
            Email = create.Email,
            Location = create.Location
        };
    }

    public void UpdateModel(User model, UserUpdateDTO update)
    {
        model.FirstName = update.FirstName;
        model.LastName = update.LastName;
        model.Email = update.Email;
        model.Location = update.Location;
    }
}
