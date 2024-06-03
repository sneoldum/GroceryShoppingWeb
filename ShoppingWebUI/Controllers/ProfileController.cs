using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebUI.Models;
using ShoppingWebUI.Services;

namespace ShoppingWebUI.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize()]
        public IActionResult Index(UserService userService)
        {
            var user = userService.GetOneByUsername(Request.Cookies["username"]);
            var profile = new ProfileViewModel()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Balance = user.Balance,
                PlayedBets = user.PlayedBets,
                ComplatedBets = user.ComplatedBets
            };
            return View(profile);
        }
        [HttpPost]
        public IActionResult updateUser(UserService userService,string FirstName,string LastName)
        {
            var user = userService.GetOneByUsername(Request.Cookies["username"]);

            user.FirstName = FirstName;
            user.LastName = LastName;

            userService.Update(user);
            return RedirectToAction("Index", "Profile");
        }
    }
}
