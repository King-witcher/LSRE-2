using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LSRE2_API
{
    /// <summary>
    ///     Represents a set of matches.
    /// </summary>
    public class History : IEnumerable<Match>
    {
        List<Match> matches;

        private History(int count)
        {
            matches = new List<Match>(count);
        }

        /// <summary>
        ///     Gets an ew instance of history given a CSV list and a given playerbase.
        /// </summary>
        /// <param name="playerbase"></param>
        /// <param name="csv"></param>
        public History(PlayerBase playerbase, CSV csv)
        {
            matches = new List<Match>(csv.Lines);
            foreach (string[] line in csv)
            {
                Player winner = playerbase[line[0]];
                Player loser = playerbase[line[1]];
                Match match = new Match(loser, winner);
                matches.Add(match);
            }
        }

        /// <summary>
        /// Create a new random set of matches between the players of a given PlayerBase.
        /// </summary>
        /// <param name="playerBase">PlayerBase</param>
        /// <param name="matches">How many matches sould be created</param>
        /// <param name="seed">The RNG seed if not null; otherwise the random seed will be random.</param>
        /// <returns>A new random set of matches between the players of a given PlayerBase</returns>
        public static History CreateRandom(PlayerBase playerBase, int matches, int? seed = null)
        {
            Random rnd;
            if (seed is int value)
                rnd = new Random(value);
            else
                rnd = new Random();

            History hist = new History(matches);

            for (int i = 0; i < matches; i++)
            {
                Player winner = playerBase[rnd.Next(0, playerBase.Count)];
                Player loser;
                while ((loser = playerBase[rnd.Next(0, playerBase.Count)]) == winner)
                    ;
                Match match = new Match(loser, winner);
                hist.matches.Add(match);
            }

            return hist;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var match in matches)
                sb.AppendLine(match.ToString());
            return sb.ToString();
        }

        public IEnumerator<Match> GetEnumerator() => matches.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => matches.GetEnumerator();
    }
}
