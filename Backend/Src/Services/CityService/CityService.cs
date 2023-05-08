using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class CityService : BaseServiceName<City, CityDTO, CityDTO, CityDTO>, ICityService
{
    public CityService(ICityRepo repo, IConverter converter) : base((CityRepo)repo, converter)
    {
    }
}
