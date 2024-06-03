using Azure.Core;
using Entitiy.Concrete;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace ShoppingWebUI.Services
{
    public class BetService
    {
        public Bet GetAllMatches()
        {


            var webClient = new WebClient();
            string url = string.Concat("https://sportsbook.iddaa.com/sportsbook/events?st=1&type=0&version=0");

            string json = new WebClient().DownloadString(url);
            Bet? betData = JsonSerializer.Deserialize<Bet>(json);
            betData.json = json;
            Console.WriteLine(betData.Datas.events.Count);

            return betData;

        }

        public MatchModel GetMatchById(int id)
        {
            var webClient = new WebClient();
            string url = string.Concat($"https://sportsbook.iddaa.com/sportsbook/event/{id}");

            string json = new WebClient().DownloadString(url);
            MatchModel? betData = JsonSerializer.Deserialize<MatchModel>(json);
            betData.json = json;
            return betData;
        }

        public ResultModel CheckFinishedMatches(int date)
        {
            var webClient = new WebClient();
            string formattedDate = DateTimeOffset.FromUnixTimeSeconds(date).LocalDateTime.ToString("yyyy-MM-dd");

            string url = string.Concat("https://statistics.iddaa.com/broadage/getEventListCache?SportId=1&SearchDate=2024-06-02");
            string json = new WebClient().DownloadString(url);
            ResultModel? betData = JsonSerializer.Deserialize<ResultModel>(json);
            betData.json = json;

            return betData;
        }
        public void CalculateBetResult(string[] matchDetail, ResultModel.Match bet, string username)
        {
            var userService = new UserService();

            var user = userService.GetOneByUsername(username);

            var matchId = Convert.ToInt32(matchDetail[0]);
            var itemId = Convert.ToInt32(matchDetail[1]);
            int st = Convert.ToInt32(matchDetail[2]);
            var no = Convert.ToInt32(matchDetail[3]);
            var betAmount = Convert.ToDouble(matchDetail[4]);
            var odd = Convert.ToDouble(matchDetail[5]);
            var date = Convert.ToInt32(matchDetail[6]);
            var at = bet.at;
            var ht = bet.ht;

            var homeScore = bet.sc.ht.c;

            var awayScore = bet.sc.at.c;

            if (st == 1)
            {

                if (homeScore > awayScore)
                {
                    if (no == 1)
                    {
                        user.Balance += betAmount * odd;
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                        userService.Update(user);
                    }
                    else
                    {
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                    }
                }
                else if (homeScore == awayScore)
                {
                    if (no == 2)
                    {
                        user.Balance += betAmount * odd;
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                        userService.Update(user);
                    }
                    else
                    {
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                    }
                }
                else
                {
                    if (no == 3)
                    {
                        user.Balance += betAmount * odd;
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                        userService.Update(user);
                    }
                    else
                    {
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                    }
                }
            }
            else if (st == 89)
            {
                if (homeScore > 0 && awayScore > 0 && no == 1)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else
                {
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                }
            }
            else if (st == 91)
            {
                if (homeScore + awayScore % 2 == 0)
                {
                    if (no == 1)
                    {
                        user.Balance += betAmount * odd;
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                        userService.Update(user);
                    }
                    else
                    {
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                    }
                }
                else
                {
                    if (no == 2)
                    {
                        user.Balance += betAmount * odd;
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                        userService.Update(user);
                    }
                    else
                    {
                        user.PlayedBets.Remove(string.Join("-", matchDetail));
                        user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                    }
                }
            }
            else if (st == 698)
            {
                if (homeScore > awayScore && homeScore != 0 && awayScore != 0 && no == 1)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else if (homeScore > awayScore && awayScore == 0 && no == 2)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else if (homeScore == awayScore && homeScore > 0 && no == 3)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else if (homeScore == awayScore && homeScore == 0 && no == 4)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else if (awayScore > homeScore && homeScore != 0 && awayScore != 0 && no == 5)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else if (awayScore > homeScore && homeScore == 0 && no == 6)
                {
                    user.Balance += betAmount * odd;
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-WON" + ht + "-" + at);
                    userService.Update(user);
                }
                else
                {
                    user.PlayedBets.Remove(string.Join("-", matchDetail));
                    user.ComplatedBets.Add(string.Join("-", matchDetail) + "-LOSE" + ht + "-" + at);
                }
            }
        }
    }
}
