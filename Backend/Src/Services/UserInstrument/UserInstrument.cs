using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public class UserInstrumentService : IUserInstrumentService
{
    public Task<UserInstrument> CreateOneAsync(UserInstrumentCreateDTO request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOneAsync(Guid userId, Guid intrumentId)
    {
        throw new NotImplementedException();
    }

    public Task<UserInstrument> UpdateOneAsync(UserInstrumentUpdateDTO request)
    {
        throw new NotImplementedException();
    }
}
