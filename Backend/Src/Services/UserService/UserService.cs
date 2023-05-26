﻿using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IGenreRepo _genreRepo;
    private readonly IConverter _converter;
    public UserService(
        IGenreRepo genreRepo,
        UserManager<User> userManager,
        IConverter converter,
        PasswordHasher<User> passwordHasher
        )
    {
        _genreRepo = genreRepo;
        _userManager = userManager;
        _converter = converter;    
    }

    public async Task<ICollection<UserReadDTO>> GetAllUsersAsync(IFilterOptions? filter)
    {
        var query = _userManager.Users
                .Include(user => user.Location.City)
                .Include(user => user.Instruments)
                .Include(user => user.Genres)
                .Where(user => true);

        if (filter is BaseQueryOptions optionsFilter)
        {
            return await query
                .Select(user => _converter.ConvertReadDTO(user, default(UserReadDTO)))
                .Skip(optionsFilter.Skip)
                .Take(optionsFilter.Limit)
                .ToListAsync();
        }
        if (filter is MatchDTO matchDTO)
        {
            query = _userManager.Users.Where(user => user.Location.City.Name == matchDTO.City && user.Level == matchDTO.Level);
            if (!query.Any())
            {
                throw new Exception("No users match location and level of commitment!");
            }
            if (matchDTO.Instrument is not null)
            {
                query = query.Where(user => user.Instruments
                    .Select(instrument => new { Instrument = instrument.Instrument.Name, instrument.LookingToPlay })
                    .Any(instrument => instrument.Instrument.Contains(matchDTO.Instrument) && instrument.LookingToPlay));
            }
            if (matchDTO.Genre is not null)
            {
                query = query.Where(user => user.Genres == null || user.Genres.Any(genre => genre.Name.Contains(matchDTO.Genre)));
            }
            return await query
                .Select(user => _converter.ConvertReadDTO(user, default(UserReadDTO)))
                .Skip(matchDTO.Skip)
                .Take(matchDTO.Limit)
                .ToListAsync();
        }
        return await _userManager.Users
            .Skip(0)
            .Take(30)
            .Select(user => _converter.ConvertReadDTO(user, default(UserReadDTO)))
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

    public async Task<UserReadDTO> GetUserProfile(string userId)
    {      
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new Exception("Profile is not found");
        }
        return _converter.ConvertReadDTO(user, default(UserReadDTO));
    }
    public async Task<UserReadDTO> AddGenre(Guid userId, GenreDTO request)
    {
        var genre = await _genreRepo.GetAllAsync(new NameFilter() { Name = request.Name });
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception("Invalid user ID");

        if (genre.Any())
        {
            var genreToAdd = genre.First();
            AddGenre(genreToAdd, user);
            return _converter.ConvertReadDTO(user, default(UserReadDTO));
        }
        else
        {
            _converter.CreateModel(request, out Genre genreToAdd);
            await _genreRepo.CreateOneAsync(genreToAdd);
            AddGenre(genreToAdd, user);
            return _converter.ConvertReadDTO(user, default(UserReadDTO));
        }

        static void AddGenre(Genre genreToAdd, User user)
        {
            if (user.Genres is null)
            {
                user.Genres = new List<Genre>() { genreToAdd };
            }
            else
            {
                user.Genres.Add(genreToAdd);
            }
        }
    }
}
