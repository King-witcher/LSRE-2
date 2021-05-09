using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LSRE2
{
    /// <summary>
    /// Represents a CSV table. 
    /// </summary>
    public class CSV : IEnumerable<IEnumerable<string>>
    {
        List<List<string>> lines;

        /// <summary>
        /// How many lines the CSV has.
        /// </summary>
        public int Lines => lines.Count;

        /// <summary>
        /// Imports a new instance of CSV given a csv file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sep">Column separator</param>
        public CSV(string path, char sep = ',')
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string[] lineArray = reader.ReadToEnd().Split('\n');
                this.lines = new List<List<string>>(lineArray.Length);
                foreach (var line in lineArray)
                    this.lines.Add(new List<string>(line.Split(sep)));
            }
        }

        /// <summary>
        /// Gets a new instance of CSV given a table.
        /// </summary>
        /// <param name="matrix"></param>
        public CSV(IEnumerable<string[]> matrix)
        {
            this.lines = new List<List<string>>();
            foreach (var line in matrix)
                new List<string>(line);
        }

        /// <summary>
        /// Gets a new instance of CSV given a table.
        /// </summary>
        /// <param name="matrix">Table lines</param>
        public CSV(IEnumerable<IEnumerable<string>> matrix)
        {
            this.lines = new List<List<string>>();
            foreach (var line in matrix)
                new List<string>(line);
        }

        internal CSV(List<List<string>> matrix)
		{
            lines = matrix;
		}


        /// <summary>
        /// Exports the CSV to the file system.
        /// </summary>
        /// <param name="path">Output path</param>
        /// <param name="sep">Column separator</param>
        public void Export(string path, char sep = ',')
        {
            using (StreamWriter sw = new StreamWriter(path))
                foreach (var line in lines)
                {
                    foreach (var column in line)
                        sw.Write(column + sep);
                    sw.WriteLine();
                }
        }

		public override string ToString()
		{
            StringBuilder sb = new StringBuilder();
            foreach(var line in lines)
			{
                foreach (var attr in line)
                    sb.Append(attr + '\t');
                sb.AppendLine();
			}
            return sb.ToString();
		}

		public IEnumerator<IEnumerable<string>> GetEnumerator() => lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>lines.GetEnumerator();
    }
}
