using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSRE2
{
	class Test
	{
		static void Main()
		{
			CSV a = new CSV(@"..\..\csv\qpl_1_3_pb.csv", ';');
			Console.WriteLine(a);
			Console.ReadLine();
		}
	}
}
