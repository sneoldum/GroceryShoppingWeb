using Business.Abstract;
using Entities.DomainModels;
using Entitiy.Concrete.Dtos;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebUI.Helpers;
using ShoppingWebUI.Models;

namespace ShoppingWebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;
        private ICartSessionHelper _cartSessionHelper;
        private IProductService _productService;

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;

        }

        public IActionResult AddToCart(int productId)
        {
            //Ürünü çek
            Product product = _productService.GetById(productId).Data;

            var cart = _cartSessionHelper.GetCart("cart").Data;
            _cartService.AddtoCart(cart, product);

            _cartSessionHelper.SetCart("cart", cart);


            TempData.Add("message", product.ProductName + " added to cart.");
            //product.UnitsInStock = Convert.ToInt16(product.UnitsInStock - 1);
            return RedirectToAction("Index", "Product");
        }


        public IActionResult RemoveFromCart(int productId)
        {
            //Ürünü çek
            Product product = _productService.GetById(productId).Data;

            var cart = _cartSessionHelper.GetCart("cart").Data;

            _cartService.RemoveFromCart(cart, productId);
            _cartSessionHelper.SetCart("cart", cart);

            TempData.Add("message", product.ProductName + " deleted from cart!");
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult ReduceFromCart(int productId)
        {
            //Ürünü çek
            Product product = _productService.GetById(productId).Data;

            var cart = _cartSessionHelper.GetCart("cart").Data;

            _cartService.ReduceFromCart(cart, productId);
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Cart");
        }
        public IActionResult IncreaseFromCart(int productId)
        {
            Product product = _productService.GetById(productId).Data;

            var cart = _cartSessionHelper.GetCart("cart").Data;

            _cartService.IncreaseFromCart(cart, productId);
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Complete(Cart cart)
        {
            var model = new ShippingDetailsViewModel
            {
                ShippingDetail = new ShippingDetail()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetail shippingDetail)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", "Your order has been successfully created.");
            _cartSessionHelper.Clear();
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Index()
        {
            var model = new CartListViewModel()
            {
                Cart = _cartSessionHelper.GetCart("cart").Data
            };
            
            return View(model);
        }

    }
}
