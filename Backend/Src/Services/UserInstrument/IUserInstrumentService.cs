using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public interface IUserInstrumentService
{
    public Task<UserInstrument> CreateOneAsync(UserInstrumentDTO request);
    public Task<bool> DeleteOneAsync(Guid userId, Guid intrumentId);
    public Task<UserInstrument> UpdateOneAsync(UserInstrumentUpdateDTO request);
}
