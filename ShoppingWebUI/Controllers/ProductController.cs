using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebUI.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IAuthService _authService;

        public ProductController(IProductService productService, ICategoryService categoryService, IAuthService authService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _authService = authService;
        }
        [Authorize()]
        public IActionResult Index(int category)
        {
            
            var model = new ProductListViewModel()
            {
                Categories = _categoryService.GetAll().Data,
                CurrentCategory = Convert.ToInt32(HttpContext.Request.Query["category"]),
                
                Products = category > 0 ? (_productService.GetbyCategory(category).Data) : _productService.GetList().Data 
                
            };

            return View(model);
        }
    }
}
