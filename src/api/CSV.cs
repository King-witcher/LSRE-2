using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LSRE2
{
    /// <summary>
    ///     Represents a CSV table. 
    /// </summary>
    public class CSV : IEnumerable<string[]>
    {
        List<string[]> lines;

        /// <summary>
        ///     How many lines the CSV has.
        /// </summary>
        public int Lines => lines.Count;

        /// <summary>
        ///     Imports a new instance of CSV given a csv file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sep">Column separator</param>
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

            sr.Close();
        }

        /// <summary>
        ///     Gets a new instance of CSV given a table.
        /// </summary>
        /// <param name="lines"></param>
        public CSV(IEnumerable<string[]> lines)
        {
            this.lines = new List<string[]>();
            foreach (var line in lines)
                this.lines.Add(line);
        }

        /// <summary>
        ///     Gets a new instance of CSV given a table.
        /// </summary>
        /// <param name="lines"></param>
        public CSV(IEnumerable<IEnumerable<string>> lines)
        {
            this.lines = new List<string[]>();
            foreach (var line in lines)
            {
                var list = new List<string>();
                foreach (var obj in line)
                    list.Add(obj.ToString());
                this.lines.Add(list.ToArray());
            }
        }

        /// <summary>
        ///     Exports the CSV to the file system.
        /// </summary>
        /// <param name="path">Column separator</param>
        public void Export(string path, char sep = ';')
        {
            using (StreamWriter sw = new StreamWriter(path))
                foreach (var line in lines)
                {
                    foreach (var column in line)
                        sw.Write(column + sep);
                    sw.WriteLine();
                }
        }

        public IEnumerator<string[]> GetEnumerator() => lines.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() =>lines.GetEnumerator();
    }
}
