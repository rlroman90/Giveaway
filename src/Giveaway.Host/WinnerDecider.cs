using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giveaway.Host
{
    public class WinnerDecider
    {
        private const long Iterations = 10000;

        private Random _random;

        public WinnerDecider()
        {
            _random = new Random();
        }

        public ulong DecideWinner(ulong[] userIds)
        {
            var winner = GetWinner(userIds);

            var winnerDictionary = userIds.ToDictionary(u => u, t => 0);

            return winner;
        }

        private ulong GetWinner(ulong[] users)
        {
            var userIds = users
                .Select(t => t)
                .ToArray();

            while(userIds.Length > 1)
            {
                var userDictionary = GetUserCountDictionary(userIds);

                var maxCount = GetMaxCount(userDictionary, userDictionary.Count);

                userIds = userDictionary
                    .Where(uc => uc.Value.Count == maxCount)
                    .Select(id => id.Value.UserId)
                    .ToArray();
            }

            return userIds.Single();
        }

        private Dictionary<int, UserCount> GetUserCountDictionary(ulong[] userNames)
        {
            var userIdsLength = userNames.Length;
            var dictionary = new Dictionary<int, UserCount>();

            for (var i = 0; i < userIdsLength; i++)
                dictionary.Add(i, new UserCount(userNames[i]));

            return dictionary;
        }

        private int GetMaxCount(Dictionary<int, UserCount> users, int userLength)
        {
            for (var i = 0; i < Iterations; i++)
            {
                var iterationWinner = _random.Next() % userLength;

                users[iterationWinner].Count++;
            }

            var counts = users.Select(u => u.Value.Count);

            var maxCount = users
                .Select(u => u.Value.Count)
                .Max();

            return maxCount;
        }
    }
}
