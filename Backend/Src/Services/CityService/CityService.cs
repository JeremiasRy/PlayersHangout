using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class CityService : BaseServiceName<City, CityDTO, CityDTO, CityDTO>, ICityService
{
    public CityService(ICityRepo repo, ICityConverter converter) : base(repo, converter)
    {
    }
}
