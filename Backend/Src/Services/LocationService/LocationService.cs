namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class LocationService : BaseService<Location, LocationReadDTO, LocationCreateDTO, LocationCreateDTO>
{
    public LocationService(IBaseRepo<Location> repo, ILocationConverter converter) : base(repo, converter)
    { 
    }
}
