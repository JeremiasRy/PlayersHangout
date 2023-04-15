namespace Backend.src.Services.WantedService;

using Backend.src.DTOs.Wanted;
using Backend.src.Services.BaseService;
using Backend.Src.Models;

public interface IWantedService : IBaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{}
