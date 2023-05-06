namespace Backend.Src.Services;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class LocationService : BaseService<Location, LocationReadDTO, LocationCreateDTO, LocationCreateDTO>, ILocationService
{
    public LocationService(IBaseRepo<Location> repo, IConverter converter) : base(repo, converter)
    {
    }
}
