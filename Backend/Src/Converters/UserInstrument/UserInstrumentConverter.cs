using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Converters;

public class UserInstrumentConverter : IUserInstrumentConverter
{
    public UserInstrumentReadDTO ConvertReadDTO(UserInstrument model)
    {
        return new UserInstrumentReadDTO()
        {
            Instrument = model.Instrument.Name,
            SkillLevel = model.Skill
        };
    }

    public void CreateModel(UserInstrument model, UserInstrumentCreateDTO create)
    {
        if (create.UserId is not null && create.InstrumentId is not null)
        {
            model.UserId = (Guid)create.UserId;
            model.InstrumentId = (Guid)create.InstrumentId;
            model.LookingToPlay = create.LookingToPlay;
            model.Skill = create.SkillLevel;
        }
    }

    public void UpdateModel(UserInstrument model, UserInstrumentUpdateDTO update)
    {
        model.Skill = update.SkillLevel ?? model.Skill;
        model.LookingToPlay = update.LookingToPlay ?? model.LookingToPlay;

    }
}
