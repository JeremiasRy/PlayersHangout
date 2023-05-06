namespace Backend.Src.Services;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class CityService : BaseServiceName<City, CityDTO, CityDTO, CityDTO>, ICityService
{
    public CityService(BaseRepoName<City> repo, IConverter converter) : base(repo, converter)
    {
    }
}
