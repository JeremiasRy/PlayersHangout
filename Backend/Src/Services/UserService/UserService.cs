namespace Backend.Src.Services.UserService;

using Backend.Src.Repositories.BaseRepo;
using Backend.Src.DTOs;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    public UserService(UserManager<User> userManager, IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }

    public async Task<ICollection<UserDTO>> GetAllUsersAsync(IFilterOptions? filter)
    {
        if (filter is BaseQueryOptions optionsFilter)
        {
            return await _userManager.Users
                .Skip(optionsFilter.Skip)
                .Take(optionsFilter.Limit)
                .Select(user => UserDTO.FromUser(user))
                .ToListAsync();
        }
        if (filter is MatchDTO matchDTO)
        {
            var query = _userManager.Users.Where(user => true);
            if (matchDTO.Instruments is not null)
            {
                query = query.Where(user => user.Instruments
                    .Any(instrument => matchDTO.Instruments.Any(matchInstrument => matchInstrument.Id == instrument.InstrumentId)));
            }
            if (matchDTO.Genres is not null)
            {
                query = query.Where(user => user.Genres == null || user.Genres.Any(genre => matchDTO.Genres.Any(matchGenre => genre.Id == matchGenre.Id)));
            }
            return await query.Where(user => user.Location.City == matchDTO.City)
                .Select(user => UserDTO.FromUser(user))
                .ToListAsync();
        }
        return await _userManager.Users
            .Skip(0)
            .Take(30)
            .Select(user => UserDTO.FromUser(user))
            .ToListAsync();
    }

    public async Task<SignInResponseDTO> SignInAsync(SignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return await _jwtTokenService.GenerateToken(user);
        }
        throw new ArgumentException("Login failed!");
    }

    public async Task<User?> SignUpAsync(SignUpDTO request)
    {
        var user = new User() 
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Location = request.Location,
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return user;
        }
        return null;
    }

    public async Task<User?> UpdateUserAsync(Guid id, UpdateUserDTO request)
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
        user.Email = request.Email;
        user.LastName = request.LastName;
        user.FirstName = request.FirstName;
        user.Location = request.Location;
        await _userManager.UpdateAsync(user);

        return user;
    }
}
