using Backend.Src.Models;

namespace Backend.Src.Repositories;

public interface IUserInstrumentRepo
{
    Task<UserInstrument> CreateOneAsync(UserInstrument model);
    Task<UserInstrument> UpdateOneAsync(UserInstrument model);
    Task DeleteOneAsync(UserInstrument model);
}
