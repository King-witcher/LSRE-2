using System;
using System.Collections.Generic;
using System.Collections;

namespace LSRE2
{
    /// <summary>
    /// Represents a set of players and it's corresonding aliases.
    /// </summary>
    public class PlayerGroup : IEnumerable<Player>
    {
        List<Player> players;
        Dictionary<string, Player> aliases;

        /// <summary>
        /// Gets an instance of player given it's alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public Player this[string alias] => aliases[alias];

        /// <summary>
        /// Gets the player count in this PlayerGroup.
        /// </summary>
        public int Count => players.Count;


        /// <summary>
        /// Gets a new instance of PlayerGroup given a CSV.
        /// </summary>
        /// <remarks>
        /// First column of the csv represents the player names, and the second column represents it's corresponding aliases.
        /// </remarks>
        /// <param name="csv"></param>
        public PlayerGroup(CSV csv) : this(csv.Lines)
        {
            foreach (string[] line in csv)
                AddPlayer(new Player(line[0]), line[1]);
        }

        /// <summary>
        /// Cria uma nova instância de PlayerGroup vazia.
        /// </summary>
        /// <param name="count">Capacidade inicial de jogadores. Caso seja nulo, o limite inicial será 20.</param>
        public PlayerGroup(int? count = null)
        {
            if (count is int n)
                players = new List<Player>(n);
            else
                players = new List<Player>(20);
            aliases = new Dictionary<string, Player>();
        }

        /// <summary>
        /// Gets an instance of Player from the PlayerGroup by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Player this[int index] => players[index];

        /// <summary>
        /// Adds a new player to the PlayerGroup and binds it to an alias.
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="alias">Alias</param>
        public void AddPlayer(Player player, string alias)
        {
            players.Add(player);
            aliases[alias] = player;
        }

        /// <summary>
        /// Shifts each player's LSR an equal amount such that the average will be 0.0.
        /// </summary>
        public void CentralizeLSR()
        {
            double Σ = 0.0;
            foreach (var player in players)
                Σ += player.LSR;
            Σ /= Count;
            foreach (var player in players)
                player.LSR -= Σ;
        }

        /// <summary>
        /// Obém o jogador associado ao apelido especificado.
        /// </summary>
        /// <param name="alias">O apelido da instância a ser obtida.</param>
        /// <returns>O jogador associado ao apelido especificado.</returns>
        public Player GetPlayer(string alias)
        {
            if (aliases.TryGetValue(alias, out Player p))
                return p;
            else
                return null;
        }

        internal bool Contains(string alias) =>
            aliases.ContainsKey(alias);

        /// <summary>
        /// Gets a new instance of n PlayerGroup with players named as Player1 ... PlayerN.
        /// </summary>
        /// <param name="n">How many players</param>
        /// <returns>A new instance of n PlayerGroup with players named as Player1 ... PlayerN</returns>
        public static PlayerGroup Create(int n)
        {
            PlayerGroup pb = new PlayerGroup(n);

            for (int i = 0; i < n; i++)
            {
                Player player = new Player("Player number " + i);
                pb.players.Add(player);
            }

            return pb;
        }

        /// <summary>
        /// Runs an algorithm that estimates the rating of each player in the PlayerGroup given a history of matches.
        /// </summary>
        /// <param name="history">History of games</param>
        /// <param name="iterations">How many times the approximation algorithm should run</param>
        /// <param name="resolution">Resolution of the calculus functions</param>
        public void EstimateRatings(History history, int iterations = 20, double resolution = 0.005)
        {
            for (int i = 0; i < iterations; i++)
                foreach (var player in players)
                    player.EstimateScore(history, resolution);
            CentralizeLSR();
        }

        /// <summary>
        /// Randomize each players skill rating in a uniform distribution within a specific LSR range.
        /// </summary>
        /// <param name="lsrmin">The inclusive LSR lower bound of the range</param>
        /// <param name="lsrmax">The exclusive LSR upper bound or the range</param>
        /// <param name="seed">The random seed if not null; otherwise the seed will be random.</param>
        public void RandomizeRatings(double lsrmin, double lsrmax, int? seed = null)
        {
            Random rnd;
            if (seed is int value)
                rnd = new Random(value);
            else
                rnd = new Random();

            foreach (var player in players)
                player.LSR = lsrmin + rnd.NextDouble() * (lsrmax - lsrmin);
        }

        public IEnumerator<Player> GetEnumerator() => players.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => players.GetEnumerator();

        public override string ToString()
        {
            string str = string.Empty;
            foreach (var player in players)
                str += player + "\n";
            return str;
        }
    }
}
