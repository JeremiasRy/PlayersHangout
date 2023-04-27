namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using System.Threading.Tasks;

public class LocationService : BaseService<Location, LocationReadDTO, LocationCreateDTO, LocationCreateDTO>
{
    private readonly IBaseService<City, City, City, City> _cityService; 
    public LocationService(IBaseService<City, City, City, City> cityService, IBaseRepo<Location> repo, ILocationConverter converter) : base(repo, converter)
    {
        _cityService = cityService;
    }
    public async override Task<LocationReadDTO> CreateAsync(LocationCreateDTO request)
    {
        if (request.CityId is not null)
        {
            return await base.CreateAsync(request);
        }
        if (request.City is not null)
        {
            var city = await _cityService.GetAllAsync(new NameFilter() { Name = request.City });
            if (!city.Any())
            {
                var createdCity = await _cityService.CreateAsync(new City() { Name = request.City });
                request.CityId = createdCity.Id;
                return await base.CreateAsync(request);
            } else
            {
                request.CityId = city.FirstOrDefault() == null ? throw new Exception("Something went horribly wrong") : city.First().Id;
                return await base.CreateAsync(request);
            }
        }
        throw new Exception("Request was malformatted");
    }
}
