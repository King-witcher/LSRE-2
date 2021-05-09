using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSRE2;

class Example
{
	static void Main()
	{
		CSV csv = new CSV(@"..\..\..\csv\qpl_1_3_hs.csv");
		History h = new History(csv);
		PlayerGroup pg = h.PlayerGroup;
		pg.EstimateRatings(h, 20, 0.005);
		var query = from player in pg
					orderby player.LSR descending
					select (player.Name, player.Elo);

		foreach (var item in query)
		{
			Console.WriteLine(item);
		}
		Console.ReadKey();
	}
}
