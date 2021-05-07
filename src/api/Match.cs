using System;
using System.Collections.Generic;
using System.Text;

namespace LSRE2
{
    public class Match
    {
        public readonly Player winner;
        public readonly Player loser;

        public Match(Player loser, Player winner)
        {
            this.loser = loser;
            this.winner = winner;
        }

        public double GetLikelihood() => winner.Q / (winner.Q + loser.Q);

        public override string ToString()
        {
            return "[" + winner + " >> " + loser + "]";
        }
    }
}
