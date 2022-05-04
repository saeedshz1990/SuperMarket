using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Services.Categories.Contracts;

namespace SuperMarket.WebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;
        public CategoriesController(CategoryService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public void Add(AddCategoryDto dto)
        {
            _service.Add(dto);
        }
    }
}
