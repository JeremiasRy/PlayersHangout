namespace Backend.Src.Services.UserService;

using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Converter.User;
using Backend.Src.DTOs.Filter;
using Backend.Src.Services.ClaimService;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserConverter _converter;
    private readonly  IClaimService _claim;
    public UserService(UserManager<User> userManager, IJwtTokenService jwtTokenService, IUserConverter converter,  IClaimService claim)
    {
        _jwtTokenService = jwtTokenService;
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
            var query = _userManager.Users.Where(user => user.Location.City == matchDTO.City);
            if (!query.Any()) 
            {
                throw new Exception("No users in this location!");
            }
            if (matchDTO.Instruments is not null)
            {
                query = query.Where(user => user.Instruments
                    .Any(instrument => matchDTO.Instruments.Any(matchInstrument => matchInstrument.Id == instrument.InstrumentId)));
            }
            if (matchDTO.Genres is not null)
            {
                query = query.Where(user => user.Genres == null || user.Genres.Any(genre => matchDTO.Genres.Any(matchGenre => genre.Id == matchGenre.Id)));
            }
            return await query
                .Select(user => _converter.ConvertReadDTO(user))
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
}
