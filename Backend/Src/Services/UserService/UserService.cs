﻿using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IConverter _converter;
    public UserService(
        UserManager<User> userManager,
        IConverter converter
        )
    {
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
                .Select(user => _converter.ConvertReadDTO(user, default(UserReadDTO)))
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
