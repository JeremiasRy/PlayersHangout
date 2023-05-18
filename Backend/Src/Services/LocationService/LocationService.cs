namespace Backend.Src.Services;

using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;

public class LocationService : BaseService<Location, LocationDTO>, ILocationService
{
    public LocationService(IConverter converter, AppDbContext appDbContext) : base(converter, appDbContext)
    {
    }
}
