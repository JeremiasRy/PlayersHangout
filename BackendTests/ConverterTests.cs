namespace BackendTests;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
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
                        Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
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
                    Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    Name = "I am initial genre",
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                }
            },
        };

        converter.CreateModel(wantedCreateDTO, out Wanted model);
        Assert.NotNull(model);
        Assert.Equal(wantedCreateDTO.Description, model.Description);
        Assert.Equal(wantedCreateDTO.Genres.First().Name, model.Genres.First().Name);
        Assert.NotNull(model.User.Location);
        Assert.Equal(wantedCreateDTO.User.Location.City.Name, model.User.Location.City.Name);
        
        WantedUpdateDTO wantedUpdateDTO = new()
        {
            Description = "I have been changed",
            Fullfilled = true
        };
        converter.UpdateModel(model, wantedUpdateDTO);
        Assert.True(model.Fullfilled);
        Assert.Equal(wantedUpdateDTO.Description, model.Description);

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

    }
}
