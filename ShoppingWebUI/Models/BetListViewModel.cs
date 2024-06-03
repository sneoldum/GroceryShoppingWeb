using Entitiy.Concrete;

namespace ShoppingWebUI.Models
{
    public class BetListViewModel
    {
        public Bet Data { get; set; }
        public Dictionary<int, MatchModel.Data> MatchDetails { get; set; }
        public double BetAmount { get; set; }
        public double Balance { get; set; }
        public double Odd { get; set; }
        public int No { get; set; }
        public int ItemId { get; set; }
        public int date { get; set; }
        public int st { get; set; }
        public int matchId { get; set; }


    }
}
