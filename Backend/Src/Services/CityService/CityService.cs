namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class CityService : BaseService<City, CityDTO, CityDTO, CityDTO>
{
    public CityService(IBaseRepo<City> repo, ICityConverter converter) : base(repo, converter)
    {
    }
}
