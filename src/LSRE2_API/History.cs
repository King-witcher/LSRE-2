using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LSRE2_API
{
    public class History : IEnumerable<Match>
    {
        List<Match> matches;

        private History(int count)
        {
            matches = new List<Match>(count);
        }

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

        private double GetLikelihood()
        {
            double π = 1.0;
            foreach (var match in matches)
                π *= match.GetLikelihood();
            return π;
        }

        private static History CreateRandom(PlayerBase set, int matches, Random rnd)
        {
            History hist = new History(matches);

            for (int i = 0; i < matches; i++)
            {
                Player winner = set[rnd.Next(0, set.Count)];
                Player loser;
                while ((loser = set[rnd.Next(0, set.Count)]) == winner)
                    ;
                Match match = new Match(loser, winner);
                hist.matches.Add(match);
            }

            return hist;
        }

        public static History CreateRandom(PlayerBase set, int matches) =>
            CreateRandom(set, matches, new Random());

        public static History CreateRandom(PlayerBase playerbase, int matches, int seed) =>
            CreateRandom(playerbase, matches, new Random(seed));

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
