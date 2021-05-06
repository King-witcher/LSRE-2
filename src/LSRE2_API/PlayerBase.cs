using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace LSRE2_API
{
    public class PlayerBase : IEnumerable<Player>
    {
        List<Player> players;
        Dictionary<string, Player> aliases = new Dictionary<string, Player>();

        public Player this[string alias] => aliases[alias];

        public int Count => players.Count;

        private PlayerBase(int count)
        {
            players = new List<Player>(count);
        }

        public PlayerBase(CSV csv)
        {
            players = new List<Player>(csv.Lines);
            foreach (string[] line in csv)
                AddPlayer(new Player(line[0]), line[1]);
        }

        public Player this[int index] => players[index];

        public void AddPlayer(Player player, string alias)
        {
            players.Add(player);
            aliases[alias] = player;
        }

        public static PlayerBase Create(int count)
        {
            PlayerBase pb = new PlayerBase(count);

            for (int i = 0; i < count; i++)
            {
                Player player = new Player("Player number " + i);
                pb.players.Add(player);
            }

            return pb;
        }

        public void EstimateRatings(History history, int iterations = 20, double resolution = 0.01)
        {
            for (int i = 0; i < iterations; i++)
                foreach (var player in players)
                    player.EstimateScore(history, resolution);
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
