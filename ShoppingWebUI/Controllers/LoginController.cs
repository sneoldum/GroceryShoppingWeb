
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entitiy.Concrete.Dtos;
using Entitiy.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebUI.Models;

namespace ShoppingWebUI.Controllers
{

    public class LoginController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;


        public LoginController(IAuthService authService, IUserService userService,IHttpContextAccessor contextAccessor)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.IsSuccess)
            {
                return RedirectToAction("Index", "Login");
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.IsSuccess)
            {
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userToLogin.Data.Id))
                };
                var identity = new ClaimsIdentity(claims, "site");
                
                _contextAccessor.HttpContext!.SignInAsync(new GenericPrincipal(identity,null)).Wait();
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Login");

        }

        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.IsSuccess)
            {
                TempData["Error"] = "User already exists !";
                return RedirectToAction("Index", "Login");
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.IsSuccess)
            {
                TempData.Add("message", "The user has been successfully created.");
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Logout(UserDetail userDetail)
        {
            //var claims = new List<Claim>() {
            //    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userDetail.Id)),
            //};
            //var identity = new ClaimsIdentity(claims, "site");
            //_contextAccessor.HttpContext.SignOutAsync(_contextAccessor.HttpContext.TraceIdentifier).Wait();   
            if (HttpContext.Request.Cookies.Count > 0)
            {
                var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                foreach (var cookie in siteCookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}