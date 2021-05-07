using System;
using System.Linq;
using System.Collections.Generic;

namespace LSRE2
{
    class Program
    {
        static void Main(string[] args)
        {
            var pbcsv = new CSV(@"..\..\csv\qpl_1_3_pb.csv");
            var hscsv = new CSV(@"..\..\csv\qpl_1_3_hs.csv");

            var pb = new PlayerBase(pbcsv);
            var hs = new History(pb, hscsv);

            pb.RandomizeRatings(-3, 3, 2);
            pb.EstimateRatings(hs, 3);

            string[,] a;

            var query = from player in pb
                        orderby player.LSR descending
                        select ( player.Name, Math.Round(player.Elo), Math.Round(100 * player.P0) );

            var matrix = from player1 in pb
                         orderby player1.LSR descending
                         select player1.Name;

            foreach (var line in query)
            {
                Console.WriteLine(line);
            }
        }
    }
}
