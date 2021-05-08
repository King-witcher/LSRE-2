using System;
using System.Linq;

namespace LSRE2
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public class Player
    {
        private readonly string name;

        private double q = 1.0;
        private double lsr = 0.0;
        private double p0 = 0.5;

        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name => name;

        /// <summary>
        /// e raised to the LSR score.
        /// </summary>
        public double Q
        {
            get => q;
            set
            {
                q = value;
                lsr = Math.Log(q);
                p0 = q / (1 + q);
            }
        }

        /// <summary>
        /// Lanna Skill Rating.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Lanna Skill Rating is an alternative to Elo Skill Rating created by me that uses Euler's base instead of a random and complex multiplication, making calculations easier to make.
        /// </para>
        /// <para>
        ///     The likelihood that a player would win another player is given by 1 / (1 + e^(LSR difference)).
        /// </para>
        /// <para>
        ///     LSR can be written as (Elo - 1500) * ln(10) / 400.
        /// </para>
        /// </remarks>
        public double LSR
        {
            get => lsr;
            set
            {
                lsr = value;
                q = Math.Exp(lsr);
                p0 = q / (1 + q);
            }
        }

        /// <summary>
        /// Likelihood that the player would win against another player with 0 LSR.
        /// </summary>
        public double P0
        {
            get => p0;
            set
            {
                p0 = value;
                q = p0 / (1 - p0);
                lsr = Math.Log(q);
            }
        }

        /// <summary>
        /// The elo rating.
        /// </summary>
        public double Elo => LSR * 173.717793 + 1500;

        /// <summary>
        /// Gets a new player with initial scores and a given name.
        /// </summary>
        /// <param name="name">Name for the player.</param>
        public Player(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Likelihood that the player would win agasint another.
        /// </summary>
        /// <param name="opponent">Another</param>
        /// <returns>Likelihood</returns>
        public double ExpectedResult(Player opponent) =>
            Q / (Q + opponent.Q);

        internal double GetLikelihoodQ(History history, double q)
        {
            var losers = from Match match in history
                         where match.winner == this
                         select match.loser;

            var winners = from Match match in history
                          where match.loser == this
                          select match.winner;
            double π = 1.0;

            foreach (var loser in losers)
                π *= q / (q + loser.Q);
            foreach (var winner in winners)
                π *= winner.Q / (q + winner.Q);

            return π;
        }

        /// <summary>
        /// Estimate the score of the player given a history and updates it's score.
        /// </summary>
        /// <param name="history">History of matches</param>
        /// <param name="dx">Calculus resolution</param>
        /// <returns>The estimated score.</returns>
        public double EstimateScore(History history, double dx = 0.0005)
        {
            double getQ(double P0) =>
                P0 / (1 - P0);

            double likelihood(double P0) =>
                GetLikelihoodQ(history, getQ(P0));

            Q = getQ(Calculus.Average01(likelihood, dx));

            return Q;
        }

        public override string ToString() => Name;
    }
}
