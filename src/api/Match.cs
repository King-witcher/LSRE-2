using System;
using System.Collections.Generic;
using System.Text;

namespace LSRE2
{
    /// <summary>
    /// Represents a match between two players.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// The winner of the match
        /// </summary>
        public readonly Player winner;

        /// <summary>
        /// The loser of the match
        /// </summary>
        public readonly Player loser;

        /// <summary>
        /// Gets a new instance of match.
        /// </summary>
        /// <param name="loser"></param>
        /// <param name="winner"></param>
        public Match(Player loser, Player winner)
        {
            this.loser = loser;
            this.winner = winner;
        }

        /// <summary>
        /// Gets how likely was that result to happen given each player's score.
        /// </summary>
        /// <returns></returns>
        public double GetLikelihood() => winner.Q / (winner.Q + loser.Q);

        public override string ToString()
        {
            return "[" + winner + " >> " + loser + "]";
        }
    }
}
