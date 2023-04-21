namespace Backend.Src.Services.UserService;

using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Src.Converter.User;
using Backend.Src.DTOs.Filter;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserConverter _converter;
    public UserService(UserManager<User> userManager, IJwtTokenService jwtTokenService, IUserConverter converter)
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
        _converter = converter;
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

    public async Task<TokenDTO> SignInAsync(SignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return await _jwtTokenService.GenerateToken(user);
        }
        throw new ArgumentException("Login failed!");
    }

    public async Task<User?> SignUpAsync(UserCreateDTO request)
    {
        var user = new User();
        _converter.CreateModel(user, request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return user;
        }
        return null;
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
}
