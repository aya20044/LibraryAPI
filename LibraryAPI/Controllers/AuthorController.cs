using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : GenericController<Author,AuthorDto>
    {
        public AuthorController(IAuthorService authorService)
            : base(authorService)
        {
        }
       
    }

}
