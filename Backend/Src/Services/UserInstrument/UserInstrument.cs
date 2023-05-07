using Backend.Src.DTOs;

namespace Backend.Src.Services;

public class UserInstrumentService : IUserInstrumentService
{
    public Task<UserInstrumentReadDTO> CreateOneAsync(UserInstrumentCreateDTO request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOneAsync(Guid userId, Guid intrumentId)
    {
        throw new NotImplementedException();
    }

    public Task<UserInstrumentReadDTO> UpdateOneAsync(UserInstrumentUpdateDTO request)
    {
        throw new NotImplementedException();
    }
}
