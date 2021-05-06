using System;
using LSRE2_API;
using System.Linq;
using System.Runtime.InteropServices;

namespace LSRE_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var pbcsv = new CSV(@"..\..\..\..\csv\qpl_1_3_pb.csv");
            var hscsv = new CSV(@"..\..\..\..\csv\qpl_1_3_hs.csv");

            var pb = new PlayerBase(pbcsv);
            var hs = new History(pb, hscsv);

            pb.EstimateRatings(hs);

            var query = from player in pb
                        orderby player.LSR descending
                        select new { player.Name, player.Elo };

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
