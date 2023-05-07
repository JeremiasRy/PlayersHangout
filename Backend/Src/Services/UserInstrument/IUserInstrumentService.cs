using Backend.Src.DTOs;

namespace Backend.Src.Services;

public interface IUserInstrumentService
{
    public Task<UserInstrumentReadDTO> CreateOneAsync(UserInstrumentCreateDTO request);
    public Task<bool> DeleteOneAsync(Guid userId, Guid intrumentId);
    public Task<UserInstrumentReadDTO> UpdateOneAsync(UserInstrumentUpdateDTO request);
}
