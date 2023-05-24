namespace BackendTests;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using System;
using System.Collections.Generic;

public class ConverterTests
{
    [Fact]
    public virtual void ConverterWanted()
    {
        Converter converter = new();
        WantedCreateDTO wantedCreateDTO = new()
        {
            Instrument = new Instrument()
            {
                Id = Guid.NewGuid(),
                Name = "test",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            SkillLevel = UserInstrument.SkillLevel.Intermediate,
            Description = "I am initial description",
            User = new User()
            {
                Location = new Location()
                {
                    City = new City()
                    {
                        Name = "test city",
                        Id = Guid.NewGuid(),
                        UpdatedAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                    },
                    Latitude = 60.0,
                    Longitude = 69.0
                }
            },
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "I am initial genre",
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "I am second genre",
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                }
            },
        };

        converter.CreateModel(wantedCreateDTO, out Wanted model);
        TestDifferentObjectsWithCommonProperties(wantedCreateDTO, model);
        
        WantedUpdateDTO wantedUpdateDTO = new()
        {
            Description = "I have been changed",
            Fullfilled = true
        };
        converter.UpdateModel(model, wantedUpdateDTO);
        TestDifferentObjectsWithCommonProperties(wantedUpdateDTO, model);

        WantedReadDTO wantedReadDTO = converter.ConvertReadDTO<Wanted, WantedReadDTO>(model);
        Assert.Equal(model.User.Location.City.Name, wantedReadDTO.City);
        Assert.Equal(model.Instrument.Name, wantedReadDTO.Instrument);
        Assert.Equal(model.Description, wantedReadDTO.Description);
        Assert.Equal(model.SkillLevel, wantedReadDTO.SkillLevel);
        if (model.Genres is not null && wantedReadDTO.Genres is not null)
        {
            foreach (var genre in model.Genres)
            {
                Assert.Contains(genre, wantedReadDTO.Genres);
            }
        }
    }
    [Fact]
    public void ConverterUser()
    {
        Converter converter = new();
        
        City city = new City()
        {
            Id = Guid.NewGuid(),
            Name = "I am city",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        Location location = new Location()
        {
            Id = Guid.NewGuid(),
            Latitude = 0,
            Longitude = 0,
            City = city,
            CityId = city.Id,
        };

        ICollection<Genre> genres = new List<Genre>()
        {
            new Genre()
            {
                Id = Guid.NewGuid(),
                Name = "Test genre 1",
                CreatedAt= DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Genre()
            {
                Id = Guid.NewGuid(),
                Name = "Test genre 2",
                CreatedAt= DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
        };

        Instrument instrument = new Instrument()
        {
            Id = Guid.NewGuid(),
            Name = "I am instrument",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        User mockUser = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Test First Name",
            LastName = "Test Last Name",
            Email = "email",
            PasswordHash = "123456wqert",
            PhoneNumber = "1234567890",
            AccessFailedCount = 0,
            ActiveSession = false,
            ConcurrencyStamp = "concurrency_stamp",
            EmailConfirmed = true,
            LockoutEnabled = true,
            Location = location,
            LocationId = location.Id,
            Genres = genres,
            LockoutEnd = DateTime.Now,
            NormalizedEmail = "EMAIL",
            NormalizedUserName = "TEST FIRST NAME",
            PhoneNumberConfirmed = true,
            SecurityStamp = "security_stamp",
            TwoFactorEnabled = false,
            UserName = "So Nice",
            Instruments = new List<UserInstrument>(),
            Wanteds = new List<Wanted>(),
        };
        UserInstrument userInstrument = new UserInstrument()
        {
            UserId = mockUser.Id,
            User = mockUser,
            InstrumentId = instrument.Id,
            Instrument = instrument,
            LookingToPlay = true,
            Skill = UserInstrument.SkillLevel.Experienced
        };
        Wanted wanted = new Wanted()
        {
            UserId = mockUser.Id,
            User = mockUser,
            Instrument = new Instrument()
            {
                Id = Guid.NewGuid(),
                Name = "Wanted Instrument",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }
        };
        mockUser.Wanteds.Add(wanted);
        mockUser.Instruments.Add(userInstrument);
        UserReadDTO result = converter.ConvertReadDTO<User, UserReadDTO>(mockUser);
        TestDifferentObjectsWithCommonProperties(result, mockUser);
    }
    [Fact]
    public void ConverterCity()
    {
        var converter = new Converter();
        CityDTO cityDTO = new CityDTO()
        {
            Name = "Initial City"
        };
        converter.CreateModel(cityDTO, out City city);
        TestDifferentObjectsWithCommonProperties(cityDTO, city);
        cityDTO.Name = "Changed city";
        converter.UpdateModel(city, cityDTO);
        TestDifferentObjectsWithCommonProperties(cityDTO, city);
    }
    [Fact]
    public void ConverterLocation()
    {
        var converter = new Converter();
        LocationCreateDTO locationCreateDTO = new LocationCreateDTO()
        {
            CityId = Guid.NewGuid(),
            City = null,
            Latitude = 10,
            Longitude = 10,
        };
        converter.CreateModel(locationCreateDTO, out Location model);
        Assert.Equal(model.CityId, locationCreateDTO.CityId);
        Assert.Equal(model.Latitude, locationCreateDTO.Latitude);
        Assert.Equal(model.Longitude, locationCreateDTO.Longitude);

    }
    static void TestDifferentObjectsWithCommonProperties(object obj1, object obj2)
    {
        foreach (var property in obj1.GetType().GetProperties())
        {
            var equalToThisProperty = obj2.GetType().GetProperty(property.Name);
            if (equalToThisProperty is not null)
            {
                Assert.Equal(property.GetValue(obj1), equalToThisProperty.GetValue(obj2));
            }
        }
    }
}
