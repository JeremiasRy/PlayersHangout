using Backend.Src.DTOs;
using Backend.Src.Models;

namespace Backend.Src.Services;

public interface ILocationService : IBaseService<Location, LocationReadDTO, LocationCreateDTO, LocationCreateDTO>
{}