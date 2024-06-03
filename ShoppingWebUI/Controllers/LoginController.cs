using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Business.Abstract;
using Entitiy.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
//using NoBet.Services;
using ShoppingWebUI.Helpers;
using ShoppingWebUI.Models;
using ShoppingWebUI.Services;

namespace ShoppingWebUI.Controllers
{
    public class LoginController : Controller
    {
        //private readonly IAuthService _authService;
        //private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginController(/*IAuthService authService, IUserService userService,*/ IHttpContextAccessor contextAccessor)
        {
            //    _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            //    _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserService userService, string UserName, string Password)
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                try
                {
                    Console.WriteLine("connected");
                    var userToCheck = userService.GetOneByUsername(UserName);
                    if (userToCheck != null)
                    {
                        if (HashingHelper.VerifyPasswordHash(Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                        {
                            var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userToCheck.Id))
                            };
                            var identity = new ClaimsIdentity(claims, "site");

                            _contextAccessor.HttpContext!.SignInAsync(new GenericPrincipal(identity, null)).Wait();
                            CookieOptions options = new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(7)
                            };

                            Response.Cookies.Append("username", UserName, options);

                            return RedirectToAction("Index", "Match");
                        }
                        else
                        {
                            ViewBag.UserLoginEx = "Please check your password or Username!";
                        }
                    }
                    else
                    {
                        ViewBag.UserLoginEx = "User not exists!";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.UserLoginEx = e.ToString();
                    throw;
                }
            }
            Console.WriteLine(ViewBag.UserLoginEx);
            return View("Index");
        }

        [HttpPost]
        public IActionResult Register(string UserName, string Password, string FirstName, string LastName, UserService userService)
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                byte[] userHash, userSalt;
                HashingHelper.CreatePasswordHash(Password, out userHash, out userSalt);
                var user = new UserMongoModel
                {
                    Username = UserName,
                    FirstName = FirstName,
                    LastName = LastName,
                    PasswordHash = userHash,
                    PasswordSalt = userSalt,
                    Status = true,
                    Balance = 10000,
                    PlayedBets = new List<string>(),
                    ComplatedBets = new List<string>()
                };
                userService.Create(user);
                
                ViewBag.UserRegisterEx = "User registered successfully!";
            }
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
