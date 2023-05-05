using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Db;

public static class ModelBuilderInsertExtensions
{
    public static void InsertGenres(this ModelBuilder modelBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("InitValues/genres.json")
            .Build();

        var genres = configuration.GetSection("genres").Get<string[]>();
        foreach (var genreName in genres)
        {
            var genre = new Genre { Id = Guid.NewGuid(), Name = genreName };
            modelBuilder.Entity<Genre>().HasData(genre);
        }        
    }

    public static void InsertInstruments(this ModelBuilder modelBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("InitValues/instruments.json")
            .Build();

        var instruments = configuration.GetSection("instruments").Get<string[]>();
        foreach (var instrumentName in instruments)
        {
            var instrument = new Instrument { Id = Guid.NewGuid(), Name = instrumentName };
            modelBuilder.Entity<Instrument>().HasData(instrument);
        }        
    }

    public static void InsertCity(this ModelBuilder modelBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("InitValues/city.json")
            .Build();
        
        foreach (var city in configuration.GetSection("cities").Get<string[]>())
        {
            var newLocation = new City
            {
                Id = Guid.NewGuid(),
                Name = city
            };
            modelBuilder.Entity<City>().HasData(newLocation);            
        }      
    }
}
