using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ILocationRepo _locationRepo;
    private readonly ICityRepo _cityRepo;
    private readonly IConverter _converter;
    private readonly IJwtTokenService _tokenService;
    private readonly AppDbContext _context;

    public AuthService(IConverter converter, ICityRepo cityRepo, ILocationRepo locationRepo, UserManager<User> userManager, IJwtTokenService tokenService, AppDbContext context)
    {
        _converter = converter;
        _locationRepo = locationRepo;
        _cityRepo = cityRepo;
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;        
    }

    public async Task<AuthReadDTO?> Login(AuthSignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception("The Email is not valid"); ;

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Wrong Password");
        }

        if (user.ActiveSession)
        {
            throw new Exception("There is another session active");
        }

        user.ActiveSession = true;
        await _userManager.UpdateAsync(user);
        return await _tokenService.GenerateToken(user);
    }

    public async Task<AuthReadDTO> SignUp(AuthSignUpDTO request)
    {
        City? city;
        if (request.CityId is null && request.City is not null)
        {
            var cities = await _context.Cities.Where(city => city.Name.Contains(request.City)).ToListAsync();
            if (cities.Any())
            {
                city = cities.First();
            }
            else
            {
                city = new City() { Name = request.City };
                await _context.AddAsync(city);
            }
        }
        else if (request.CityId is not null && request.City is null)
        {
            city = await _context.Cities.SingleOrDefaultAsync(city => city.Id == (Guid)request.CityId) ?? throw new Exception("Did not find requested city from DB");
        }
        else
        {
            throw new Exception("City was not provided correctly use only cityId or city name");
        }

        _converter.CreateModel(new LocationCreateDTO() { Latitude = request.Latitude, Longitude = request.Longitude }, out Location location);
        location.City = city;

        await _context.AddAsync(location);

        var user = new User
        {
            UserName = request.Email,
            FirstName = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Location = location,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.ToList()[0].Description ?? "Error Registering User");
        }

        user.ActiveSession = true;
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
        return await _tokenService.GenerateToken(user);
    }

    public async Task<bool> Logout(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User is not found"); ;
        user.ActiveSession = false;
        await _userManager.UpdateAsync(user);
        return true;
    }
}