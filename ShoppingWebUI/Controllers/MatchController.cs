using Entitiy.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingWebUI.Models;
using ShoppingWebUI.Services;

namespace ShoppingWebUI.Controllers
{
    public class MatchController : Controller
    {
        [Authorize()]
        public IActionResult Index()
        {
            var betService = new BetService();
            var user = new UserService();
            var balance = user.GetOneByUsername(Request.Cookies["username"]).Balance;

            var events = new BetListViewModel()
            {

                Data = betService.GetAllMatches(),

                MatchDetails = new Dictionary<int, MatchModel.Data>(),
                Balance = balance
            };

            return View(events);

        }

        [HttpPost]
        public IActionResult PlaceBet(int itemId, double odd, int no, double betAmount, int matchId, int date,int st, UserService userService)
        {
            // Kuponu veritabanına kaydetme işlemi burada yapılacak
            // cookie den kullanıcı bilgileri alınacak
            var userName = Request.Cookies["username"];

            UserMongoModel user = userService.GetOneByUsername(userName);

            user.Balance -= betAmount;

            var playedBetString = matchId + "-" + itemId +"-"+st+ "-" + no + "-" + betAmount + "-" + odd + "-" + date;
            user.PlayedBets.Add(playedBetString);

            // Kullanıcı bilgilerini güncelleme işlemi burada yapılacak

            userService.Update(user);


            return RedirectToAction("Index", "Profile");
        }

    }
}
