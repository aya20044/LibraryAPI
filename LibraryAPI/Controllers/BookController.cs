using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookController : GenericController<Book,BookDto>
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
            : base(bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> Search([FromQuery]string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest();
            var books = await _bookService.Search(searchTerm);
            return Ok(books);
        }
        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<BookDto>>> Sort([FromQuery]string? sortDirection)
        {
            var books = await _bookService.SortByTitle(sortDirection);
            return Ok(books);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest();
            var books = await _bookService.GetPaged(pageNumber, pageSize);
            return Ok(books);

        }
    }
}
