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

    public void CreateModel(UserInstrumentCreateDTO create, out UserInstrument model)
    {
        
        if (create.UserId is null || create.InstrumentId is null)
        {
            throw new Exception("Cant' create model with IDs");
        } else
        {
            model = new UserInstrument()
            {
                UserId = (Guid)create.UserId,
                InstrumentId = (Guid)create.InstrumentId,
                LookingToPlay = create.LookingToPlay,
                Skill = create.SkillLevel
            };
        }
    }

    public void UpdateModel(UserInstrument model, UserInstrumentUpdateDTO update)
    {
        model.Skill = update.SkillLevel ?? model.Skill;
        model.LookingToPlay = update.LookingToPlay ?? model.LookingToPlay;

    }
}
