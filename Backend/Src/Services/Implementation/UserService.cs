using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Services.Implementation;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    public UserService(UserManager<User> userManager, IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }
    public async Task<ICollection<UserDTO>> GetAllUsersAsync(int page, int pageSize)
    {
        return await _userManager.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
