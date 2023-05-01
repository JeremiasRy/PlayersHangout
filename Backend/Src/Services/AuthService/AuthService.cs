namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IBaseRepo<Location> _locationRepo;
    private readonly IBaseRepo<City> _cityRepo;
    private readonly ILocationConverter _locationConverter;
    private readonly IJwtTokenService _tokenService;
    private readonly IClaimService _claim;

    public AuthService(ILocationConverter locationConverter, IBaseRepo<City> cityRepo, IBaseRepo<Location> locationRepo, UserManager<User> userManager, IJwtTokenService tokenService, IClaimService claim)
    {
        _locationConverter = locationConverter;
        _locationRepo = locationRepo;
        _cityRepo = cityRepo;
        _userManager = userManager;
        _tokenService = tokenService;
        _claim = claim;
    }

    public async Task<AuthReadDTO?> Login(AuthSignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new Exception("The Email is not valid");
        }

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
            var cities = await _cityRepo.GetAllAsync(new NameFilter() { Name = request.City });
            if (cities.Any())
            {
                city = cities.First();
            }
            else
            {
                city = await _cityRepo.CreateOneAsync(new City() { Name = request.City }) ?? throw new Exception("Invalid value for city name");
            }
        }
        else if (request.CityId is not null && request.City is null)
        {
            city = await _cityRepo.GetByIdAsync((Guid)request.CityId) ?? throw new Exception("Did not find requested city from DB");
        }
        else
        {
            throw new Exception("City was not provided correctly use only cityId or city name");
        }

        _locationConverter.CreateModel(new LocationCreateDTO() { CityId = city.Id, Latitude = request.Latitude, Longitude = request.Longitude }, out Location location);

        location = await _locationRepo.CreateOneAsync(location) ?? throw new Exception("Error while processing location data");

        var user = new User
        {
            UserName = request.Email,
            FirstName = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            LocationId = location.Id,
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.ToList()[0].Description ?? "Error Registering User");
        }

        user.ActiveSession = true;
        await _userManager.UpdateAsync(user);
        return await _tokenService.GenerateToken(user);
    }

    public async Task<bool> Logout()
    {
        var user = await _userManager.FindByIdAsync(_claim.GetUserIDFromToken());
        if (user is null)
        {
            throw new Exception("User is not found");
        }

        user.ActiveSession = false;
        await _userManager.UpdateAsync(user);
        return true;
    }
}