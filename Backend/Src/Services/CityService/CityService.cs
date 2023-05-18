using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public class CityService : BaseServiceNameFilter<City, CityDTO>, ICityService
{
    public CityService(IConverter converter, AppDbContext appDbContext) : base(converter, appDbContext)
    {
    }
}
