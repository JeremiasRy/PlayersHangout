namespace Backend.Src.Controllers;

using Backend.Src.Common;
using Backend.Src.Repositories;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
public abstract class BaseController<T, TReadDto, TCreateDto, TUpdateDto> : ApiControllerBase
{
    protected readonly IBaseService<T, TReadDto, TCreateDto, TUpdateDto> _service;
    public BaseController(IBaseService<T, TReadDto, TCreateDto, TUpdateDto> service)
    {
        _service = service;
    }
    [HttpGet]
    public virtual async Task<ICollection<TReadDto>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<BaseQueryOptions>();
        return await _service.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<ActionResult<TReadDto?>> CreateOne(TCreateDto create)
    {
        try
        {
            return await _service.CreateAsync(create);
        } catch
        {
            return BadRequest("Can't create!");
        }
        
    }

    [HttpGet("{id:int}")]
    public async Task<TReadDto?> GetById([FromRoute] Guid id)
    {
        return await _service.GetByIdAsync(id);
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> Delete(Guid id)
    {
        return await _service.DeleteAsync(id);
    }

    [HttpPut("{id:int}")]
    public async Task<TReadDto> Update(Guid id, TUpdateDto update)
    {
        return await _service.UpdateAsync(id, update);
    }
}