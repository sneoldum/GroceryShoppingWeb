using System.Reflection.PortableExecutable;
using Business.Abstract;
using Entitiy.Concrete.Dtos;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {

        private ICategoryService _categoryService;

        public CategoryApiController(ICategoryService categoryService)
        {

            _categoryService = categoryService;
        }
        [HttpGet("getbyid")]
        public IActionResult GetbyId(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);

            if (result.IsSuccess)
            {
                return Ok(result.Data);

            }
            else
            {
                return BadRequest(result.Data);
            }
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();

            if (result.IsSuccess)
            {
                return Ok(result.Data);

            }
            else
            {
                return BadRequest(result.Data);
            }

        }
        [HttpPost("add")]
        public IActionResult Add(CategoryDetail categoryDetail)
        {
            var result = _categoryService.Add(categoryDetail);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost("delete")]
        public IActionResult Delete(int productId)
        {
            var result = _categoryService.Delete(productId);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPatch("update")]
        public IActionResult Update(Category category)
        {
            var result = _categoryService.Update(category);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}