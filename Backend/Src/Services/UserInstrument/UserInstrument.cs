using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Backend.Src.Services;

public class UserInstrumentService : IUserInstrumentService
{
    private readonly IInstrumentRepo _instrumentRepo;
    private readonly UserManager<User> _userManager;  
    public UserInstrumentService(IInstrumentRepo instrumentRepo, UserManager<User> userManager)
    {
        _instrumentRepo = instrumentRepo;
        _userManager = userManager;
    }
    public async Task<UserInstrument> CreateOneAsync(UserInstrumentDTO request)
    {
        Instrument? instrument = null;
        if (request.InstrumentId is null && request.Instrument is not null) 
        {
            var result = await _instrumentRepo.GetAllAsync(new NameFilter() { Name = request.Instrument });
            instrument = result.FirstOrDefault();
        }
        if (request.Instrument is null &&  request.InstrumentId is not null) 
        {
            instrument = await _instrumentRepo.GetByIdAsync((Guid)request.InstrumentId);
        }

        if (instrument is null)
        {
            throw new ArgumentException("Can't find instrument from database");
        }
        User user = await _userManager.FindByIdAsync(request.UserId.ToString()) ?? throw new ArgumentException("Can't find user");


        UserInstrument userInstrument = new UserInstrument()
        {
            Instrument = instrument,
            InstrumentId = instrument.Id,
            User = user,
            UserId = user.Id,
            LookingToPlay = request.LookingToPlay,
            Skill = request.SkillLevel
        };
        user.Instruments.Add(userInstrument);
        await _userManager.UpdateAsync(user);
        return userInstrument;
    }

    public async Task<bool> DeleteOneAsync(Guid userId, Guid instrumentId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user.Instruments.Any(instrument => instrument.Instrument.Id == instrumentId))
        {
            user.Instruments = user.Instruments.Where(instrument => instrument.Instrument.Id != instrumentId).ToList();
            await _userManager.UpdateAsync(user);
            return true;
        };
        return false;
    }

    public async Task<UserInstrument> UpdateOneAsync(UserInstrumentDTO request)
    {
        if (request.InstrumentId == null) 
        {
            throw new ArgumentNullException(nameof(request.InstrumentId));
        }
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        var mappingObject = user.Instruments.SingleOrDefault(instrument => instrument.InstrumentId == request.InstrumentId) ?? throw new Exception("User does not have instrument");
        
        mappingObject.Skill = request.SkillLevel;
        mappingObject.LookingToPlay = request.LookingToPlay;
        await _userManager.UpdateAsync(user);
        return mappingObject;
    }
}
