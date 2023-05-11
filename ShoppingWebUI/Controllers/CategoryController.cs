using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebUI.Models;

namespace ShoppingWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {

            var category = _categoryService.GetAll().Data;

            var model = new CategoryListViewModel()
            {
                Categories = category
            };
            
            return View(model);
        }
    }
}