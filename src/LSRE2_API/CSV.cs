using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LSRE2_API
{
    public class CSV : IEnumerable<string[]>
    {
        List<string[]> lines;
        public int Lines => lines.Count;

        public CSV(string path, char sep = ';')
        {
            var sr = new StreamReader(path);
            lines = new List<string[]>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] linesep = line.Split(sep);
                lines.Add(linesep);
            }
        }

        public IEnumerator<string[]> GetEnumerator() => lines.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() =>lines.GetEnumerator();
    }
}
