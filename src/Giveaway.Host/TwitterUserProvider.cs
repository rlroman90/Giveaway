using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Giveaway.Host
{
    public class TwitterUserProvider
    {
        private SingleUserAuthorizer _authorizer;

        public TwitterUserProvider()
        {
            _authorizer = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "",
                    ConsumerSecret = "",
                    AccessToken = "",
                    AccessTokenSecret = ""
                }
            };

            _authorizer.AuthorizeAsync();
        }

        public IList<TwitterUser> GetUsersThatRetweeted(ulong tweetId)
        {
            var raverRaftingUser = GetTwitterUser("raverrafting", "RaverRafting");

            var tweet = GetTweet(raverRaftingUser, tweetId);

            return GetRetweetedUsers(tweet.StatusID);
        }

        private TwitterUser GetTwitterUser(string screenName, string name)
        {
            using (var twitterContext = new TwitterContext(_authorizer))
            {
                var user = twitterContext.User
                    .FirstOrDefault(u => u.Type == UserType.Search && u.Query == name && u.Name == name);

                if (user != null)
                    return new TwitterUser
                    {
                        UserID = Convert.ToUInt64(user.UserIDResponse),
                        Name = user.Name,
                        ScreenName = user.ScreenNameResponse
                    };
                else
                    throw new Exception("Could not find Twitter User: " + screenName);
            }
        }

        private Status GetTweet(TwitterUser user, ulong tweetId)
        {
            using (var twitterContext = new TwitterContext(_authorizer))
            {
                var tweet = twitterContext.Status
                        .FirstOrDefault(s => s.Type == StatusType.User && s.UserID == user.UserID && s.MaxID == tweetId);

                if (tweet != null)
                    return tweet;
                else
                    throw new Exception("Could not find tweet: " + tweetId + ", for user: " + user.ScreenName);
            }
        }

        private IList<TwitterUser> GetRetweetedUsers(ulong originalTweetId)
        {
            var retweets = GetRetweets(originalTweetId);

            var retweetedUsers = retweets
                .Select(r => r.User)
                .ToList();

            return retweetedUsers
                .Select(ru => new TwitterUser { UserID = Convert.ToUInt64(ru.UserIDResponse), Name = ru.Name, ScreenName = ru.ScreenNameResponse })
                .ToList();
        }

        private IList<Status> GetRetweets(ulong originalTweetId)
        {
            try
            {
                using (var twitterContext = new TwitterContext(_authorizer))
                {
                    return twitterContext.Status
                        .Where(s => s.Type == StatusType.Retweets && s.ID == originalTweetId && s.Count == 500)
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error getting retweets for tweet: " + originalTweetId, ex);
            }
        }
    }
}
