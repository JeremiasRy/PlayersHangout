namespace Backend.Src.Services;

using Backend.Src.Repositories;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Converters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserInstrumentConverter _userInstrumentConverter;
    private readonly IUserConverter _converter;
    private readonly  IClaimService _claim;
    public UserService(IUserInstrumentConverter userInstrumentConverter, UserManager<User> userManager, IUserConverter converter,  IClaimService claim)
    {
        _userInstrumentConverter = userInstrumentConverter;
        _userManager = userManager;
        _converter = converter;
        _claim = claim;
    }

    public async Task<ICollection<UserReadDTO>> GetAllUsersAsync(IFilterOptions? filter)
    {
        if (filter is BaseQueryOptions optionsFilter)
        {
            return await _userManager.Users
                .Skip(optionsFilter.Skip)
                .Take(optionsFilter.Limit)
                .Select(user => _converter.ConvertReadDTO(user))
                .ToListAsync();
        }
        if (filter is MatchDTO matchDTO)
        {
            var query = _userManager.Users.Where(user => user.Location.City.Name == matchDTO.City);
            if (!query.Any()) 
            {
                throw new Exception("No users in this location!");
            }
            if (matchDTO.Instrument is not null)
            {
                query = query.Where(user => user.Instruments
                    .Select(instrument => new { Instrument = instrument.Instrument.Name, SkillLevel = instrument.Skill, instrument.LookingToPlay })
                    .Any(instrument => instrument.Instrument.Contains(matchDTO.Instrument) && instrument.SkillLevel == matchDTO.SkillLevel && instrument.LookingToPlay));
            }
            if (matchDTO.Genre is not null)
            {
                query = query.Where(user => user.Genres == null || user.Genres.Any(genre => genre.Name.Contains(matchDTO.Genre)));
            }
            return await query
                .Select(user => _converter.ConvertReadDTO(user))
                .Skip(matchDTO.Skip)
                .Take(matchDTO.Limit)
                .ToListAsync();
        }
        return await _userManager.Users
            .Skip(0)
            .Take(30)
            .Select(user => _converter.ConvertReadDTO(user))
            .ToListAsync();
    }

    public async Task<User?> UpdateUserAsync(Guid id, UserUpdateDTO request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password)) 
        {
            return null;
        }
        if (request.NewPassword != null)
        {
            await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
        }
        _converter.UpdateModel(user, request);
        await _userManager.UpdateAsync(user);

        return user;
    }

    public async Task<UserReadDTO> GetUserProfile() 
    {
        var user = await _userManager.FindByIdAsync(_claim.GetUserIDFromToken());
        if (user is null)
        {
            throw new Exception("Profile is not found");
        }
        return _converter.ConvertReadDTO(user);
    }

    public async Task<UserReadDTO> AddInstrument(Guid userId, UserInstrumentCreateDTO request)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == userId) ?? throw new Exception("Incalid user ID");

        var user_instrument = new UserInstrument();

        _userInstrumentConverter.CreateModel(user_instrument, request);
        user.Instruments.Add(user_instrument);

        return _converter.ConvertReadDTO(user);
    }

    public Task<UserReadDTO> AddGenre(Guid userId, string genre)
    {
        throw new NotImplementedException();
    }
}
