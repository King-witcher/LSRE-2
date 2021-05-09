using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LSRE2
{
    /// <summary>
    /// Represents a set of matches.
    /// </summary>
    public class History : IEnumerable<Match>
    {
        List<Match> matches;
        PlayerGroup playerGroup;

        private History(int count)
        {
            matches = new List<Match>(count);
        }

        /// <summary>
        /// Gets an ew instance of history given a CSV list and a given PlayerGroup.
        /// </summary>
        /// <param name="PlayerGroup"></param>
        /// <param name="csv"></param>
        public History(PlayerGroup PlayerGroup, CSV csv)
        {
            matches = new List<Match>(csv.Lines);
            foreach (List<string> line in csv)
            {
                Player winner = PlayerGroup[line[0]];
                Player loser = PlayerGroup[line[1]];
                Match match = new Match(loser, winner);
                matches.Add(match);
            }
        }

        /// <summary>
        /// Obt[em uma nova instância de histórico dado uma instância de CSV.
        /// </summary>
        /// <param name="csv">A instâncica de CSV contendo o histórico de partidas.</param>
        public History(CSV csv)
        {
            PlayerGroup group = new PlayerGroup();
            List<Match> matches = new List<Match>(csv.Lines);
            void addPlayer(string name)
            {
                Player p = new Player(name);
                group.AddPlayer(p, name);
            }

            foreach (List<string> line in csv)
            {
                if (line.Count < 2)
                    continue;

                string winnerName = line[0],
                    loserName = line[1];

                if (!group.Contains(loserName))
                    addPlayer(loserName);
                if (!group.Contains(winnerName))
                    addPlayer(winnerName);

                Match match = new Match(group.GetPlayer(loserName), group.GetPlayer(winnerName));
                matches.Add(match);
            }

            this.playerGroup = group;
            this.matches = matches;
        }

        /// <summary>
        /// Obtém a instância de PlayerGroup referente ao histórico.
        /// </summary>
        public PlayerGroup PlayerGroup => playerGroup;

        /// <summary>
        /// Cria um novo conjunto aleatório de partidas entre os jogadores de uma PlayerGroup.
        /// </summary>
        /// <param name="PlayerGroup">PlayerGroup</param>
        /// <param name="matches">Quantas partidas deverão ser simuladas</param>
        /// <param name="seed">A semente de geração de números aleatórios. Caso seja null, uma semente aleatória será criada.</param>
        /// <returns>Um novo conjunto aleatório de partidas entre os jogadores de uma PlayerGroup.</returns>
        public static History CreateRandom(PlayerGroup PlayerGroup, int matches, int? seed = null)
        {
            Random rnd;
            if (seed is int value)
                rnd = new Random(value);
            else
                rnd = new Random();

            History hist = new History(matches);

            for (int i = 0; i < matches; i++)
            {
                Player winner = PlayerGroup[rnd.Next(0, PlayerGroup.Count)];
                Player loser;
                while ((loser = PlayerGroup[rnd.Next(0, PlayerGroup.Count)]) == winner)
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
