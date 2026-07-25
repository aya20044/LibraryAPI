using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : GenericController<Category,CategoryDto>
    {
        public CategoryController(ICategoryService categoryService)
            : base(categoryService)
        {

        }

    }
}
