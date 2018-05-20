using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giveaway.Host
{
    public class UserCount
    {
        public UserCount(ulong userId)
        {
            UserId = userId;

            Count = 0;
        }

        public ulong UserId { get; }

        public int Count { get; set; }
    }
}
