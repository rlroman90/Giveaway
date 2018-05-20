using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giveaway.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var giveawayService = new GiveawayService();

            while (true)
            {
                try
                {
                    giveawayService.Run();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
