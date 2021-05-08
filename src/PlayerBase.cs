using System;
using System.Collections.Generic;
using System.Collections;

namespace LSRE2
{
    /// <summary>
    /// Represents a set of players and it's corresonding aliases.
    /// </summary>
    public class PlayerBase : IEnumerable<Player>
    {
        List<Player> players;
        Dictionary<string, Player> aliases = new Dictionary<string, Player>();

        /// <summary>
        /// Gets an instance of player given it's alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public Player this[string alias] => aliases[alias];

        /// <summary>
        /// Gets the player count in this playerbase.
        /// </summary>
        public int Count => players.Count;

        private PlayerBase(int count)
        {
            players = new List<Player>(count);
        }

        /// <summary>
        /// Gets a new instance of PlayerBase given a CSV.
        /// </summary>
        /// <remarks>
        /// First column of the csv represents the player names, and the second column represents it's corresponding aliases.
        /// </remarks>
        /// <param name="csv"></param>
        public PlayerBase(CSV csv)
        {
            players = new List<Player>(csv.Lines);
            foreach (string[] line in csv)
                AddPlayer(new Player(line[0]), line[1]);
        }

        /// <summary>
        /// Gets an instance of Player from the PlayerBase by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Player this[int index] => players[index];

        /// <summary>
        /// Adds a new player to the PlayerBase and binds it to an alias.
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
        /// Gets a new instance of n PlayerBase with players named as Player1 ... PlayerN.
        /// </summary>
        /// <param name="n">How many players</param>
        /// <returns>A new instance of n PlayerBase with players named as Player1 ... PlayerN</returns>
        public static PlayerBase Create(int n)
        {
            PlayerBase pb = new PlayerBase(n);

            for (int i = 0; i < n; i++)
            {
                Player player = new Player("Player number " + i);
                pb.players.Add(player);
            }

            return pb;
        }

        /// <summary>
        /// Runs an algorithm that estimates the rating of each player in the PlayerBase given a history of matches.
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
