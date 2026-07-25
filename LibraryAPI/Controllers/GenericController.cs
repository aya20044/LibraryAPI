using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    public class GenericController <TEntity,TDto>: ControllerBase
        where TEntity:class,IEntity
    {
        protected readonly IGenericService<TEntity, TDto> Service;
        public GenericController(IGenericService<TEntity, TDto> service)
        {
            Service = service;
        }
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            var items = await Service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(int id)
        {
            var item = await Service.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Add([FromBody] TDto dto)
        {
            var item = await Service.Add(dto);
            if (item == null)
                return BadRequest();
            return Ok(item);
        }
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TDto>> Edit(int id, [FromBody] TDto dto)
        {
            var item = await Service.Edit(id, dto);
            if (item == null)
                return BadRequest();
            return Ok(item);
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var result = await Service.Delete(id);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
