using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giveaway.Host
{
    public class GiveawayService
    {
        private TwitterUserProvider _twitterUserProvider;
        private WinnerDecider _winnerDecider;
        private StatusProvider _statusProvider;

        public GiveawayService()
        {
            _twitterUserProvider = new TwitterUserProvider();
            _winnerDecider = new WinnerDecider();
            _statusProvider = new StatusProvider();
        }

        public void Run()
        {
            var statusId = _statusProvider.GetStatusIdFromConsole();

            var usersRetweeted = _twitterUserProvider.GetUsersThatRetweeted(statusId);

            var winnerId = _winnerDecider.DecideWinner(usersRetweeted.Select(ur => ur.UserID).ToArray());

            var winner = GetWinningUser(usersRetweeted, winnerId);
        }

        private TwitterUser GetWinningUser(IList<TwitterUser> twitterUsers, ulong winningId)
        {
            var winningUser = twitterUsers.FirstOrDefault(tu => tu.UserID == winningId);

            Console.WriteLine("\nWE HAVE A WINNER!!!\n");
            Console.WriteLine("UserId: " + winningUser.UserID);
            Console.WriteLine("Name: " + winningUser.Name);
            Console.WriteLine("Name: " + winningUser.ScreenName);

            return winningUser;
        }
    }
}