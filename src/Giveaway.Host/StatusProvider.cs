using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giveaway.Host
{
    public class StatusProvider
    {
        public ulong GetStatusIdFromConsole()
        {
            Console.WriteLine("\nPlease enter tweet id and Press Enter");
            var statusId = Console.ReadLine();

            var convertedStatusId = ConvertToUInt64(statusId);

            return convertedStatusId;
        }

        private ulong ConvertToUInt64(string statusIdString)
        {
            try
            {
                return Convert.ToUInt64(statusIdString);
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid Tweet Id: " + statusIdString, ex);
            }
        }
    }
}
