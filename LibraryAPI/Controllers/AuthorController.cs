using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public IAuthorService _service;
        public AuthorController(IAuthorService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _service.GetAll();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _service.GetById(id);
            if (author == null)
                return NotFound();
            return Ok(author);
        }
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Add([FromBody] AuthorDto dto)
        {
            var author = await _service.Add(dto);
            return Ok(author);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> Edit(int id, [FromBody] AuthorDto dto)
        {
            var author = await _service.Edit(id, dto);
            return Ok(author);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var hasBooks = await _service.HasBooks(id);
            if (hasBooks)
                return Conflict();
            var result = await _service.Delete(id);
            if (result)
                return Ok();
            return BadRequest();
            
        }
    }

}
