namespace Backend.Src.Controllers;

using Backend.Src.Services.BaseService;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseController<T, TReadDto, TCreateDto, TUpdateDto> : ApiControllerBase
{
    protected readonly IBaseService<T, TReadDto, TCreateDto, TUpdateDto> _service;

    public BaseController(IBaseService<T, TReadDto, TCreateDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TReadDto?>> CreateOne(TCreateDto create)
    {
        return await _service.CreateAsync(create);        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TReadDto?>> GetById([FromRoute] Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
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