namespace Backend.Src.Controllers;

using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenreController : BaseController<Genre, GenreDTO, GenreDTO, GenreDTO>
{
    public GenreController(IGenreService service) : base(service)
    {
    }
    [AllowAnonymous]
    public async override Task<ICollection<GenreDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<NameFilter>();
        return await _service.GetAllAsync(filter);
    }
}