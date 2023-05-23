using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Backend.Src.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ILocationRepo _locationRepo;
    private readonly ICityRepo _cityRepo;
    private readonly IInstrumentRepo _instrumentRepo;
    private readonly IGenreRepo _genreRepo;
    private readonly IConverter _converter;
    private readonly IJwtTokenService _tokenService;

    public AuthService(IConverter converter,IInstrumentRepo instrumentRepo, IGenreRepo genreRepo, ICityRepo cityRepo, ILocationRepo locationRepo, UserManager<User> userManager, IJwtTokenService tokenService)
    {
        _converter = converter;
        _locationRepo = locationRepo;
        _cityRepo = cityRepo;
        _instrumentRepo = instrumentRepo;
        _genreRepo = genreRepo;
        _userManager = userManager;
        _tokenService = tokenService;        
    }

    public async Task<AuthReadDTO?> Login(AuthSignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception("The Email is not valid");

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
        var city = await CityCheck(request) ?? throw new Exception("City was not properly handled");

        _converter.CreateModel(new LocationCreateDTO() { CityId = city.Id, Latitude = request.Latitude, Longitude = request.Longitude }, out Location location);

        await _locationRepo.CreateOneAsync(location);

        var user = new User
        {
            UserName = request.Name,
            FirstName = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            LocationId = location.Id,
            ActiveSession = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            await _locationRepo.DeleteOneAsync(location);
            throw new Exception(result.Errors.ToList()[0].Description ?? "Error Registering User");
        }
        user.Instruments = new List<UserInstrument>();
        user.Genres = new List<Genre>();
        if (request.Instruments is not null)
        {
            if (request.Instruments.Where(item => item.IsMain).Count() > 1) 
            {
                await _userManager.DeleteAsync(user);
                throw new Exception("You have marked two instruments as a main instrument, Check sign up form!");
            }
            foreach (var userInstrumentDto in request.Instruments)
            {
                var instrument = await InstrumentCheck(userInstrumentDto);
                if (instrument is null) 
                {
                    await _userManager.DeleteAsync(user);
                    throw new Exception($"There was an error with instrument, Check your sign up form, Name: {userInstrumentDto.Instrument}, Id: {userInstrumentDto.InstrumentId}");
                }    
                _converter.CreateModel(new UserInstrumentDTO() { InstrumentId = instrument.Id, IsMain = userInstrumentDto.IsMain, UserId = user.Id, LookingToPlay = userInstrumentDto.LookingToPlay, SkillLevel = userInstrumentDto.SkillLevel }, out UserInstrument userInstrument);
                user.Instruments.Add(userInstrument);
            }
        }
        if (request.Genres is not null)
        {
            foreach (var genreDto in request.Genres)
            {
                var genre = await GenreCheck(genreDto);
                if (genre is null)
                {
                    await _userManager.DeleteAsync(user);
                    throw new Exception($"Genre was not handled properly, Check signup form, Name {genreDto.Name}");
                }
                user.Genres.Add(genre);
            }
        }
        await _userManager.UpdateAsync(user);
        return await _tokenService.GenerateToken(user);
    }

    public async Task<bool> Logout(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User is not found"); ;
        user.ActiveSession = false;
        await _userManager.UpdateAsync(user);
        return true;
    }
    async Task<City?> CityCheck(AuthSignUpDTO request)
    {
        City? city;
        if (request.CityId is null && request.City is not null)
        {
            var cities = await _cityRepo.GetAllAsync(new NameFilter() { Name = request.City });

            city = cities.Any() 
                ? cities.First()
                : await _cityRepo.CreateOneAsync(new City() { Name = request.City });
        }
        else if (request.CityId is not null && request.City is null)
        {
            city = await _cityRepo.GetByIdAsync((Guid)request.CityId) ?? throw new Exception("Did not find requested city from DB");
        }
        else
        {
            throw new Exception("City was not provided correctly use only cityId or city name");
        }
        return city;
    }
    async Task<Instrument?> InstrumentCheck(UserInstrumentDTO request)
    {
        Instrument? instrument;
        if (request.InstrumentId is null && request.Instrument is not null)
        {
            var instruments = await _instrumentRepo.GetAllAsync(new NameFilter() { Name = request.Instrument });
            instrument = instruments.Any()
                ? instruments.First()
                : await _instrumentRepo.CreateOneAsync(new Instrument() { Name = request.Instrument });

        }
        else if (request.Instrument is null && request.InstrumentId is not null)
        {
            instrument = await _instrumentRepo.GetByIdAsync((Guid)request.InstrumentId) ?? throw new Exception("Did not find instrument from DB");
        }
        else
        {
            throw new Exception("Only send in instrument name or instrument id in request!");
        }
        return instrument;
    }
    async Task<Genre?> GenreCheck(GenreDTO request)
    {
        var genres = await _genreRepo.GetAllAsync(new NameFilter() { Name = request.Name });

        return genres.Any() 
            ? genres.First() 
            : await _genreRepo.CreateOneAsync(new Genre() { Name = request.Name });
    }
}