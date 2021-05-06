using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LSRE2_API
{
    public class Player
    {
        private readonly string name;
        //private readonly string alias;

        private double q = 1.0;     // E raised to its skill rating
        private double lsr = 0.0;   // LSR
        private double p0 = 0.5;    // Likelihood that the player would win a 0.0 lsr player.

        //public string Alias => alias;
        public string Name => name;

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
        public double LSR
        {
            get => lsr;
            set
            {
                lsr = value;
                q = lsr;
                p0 = q / (1 + q);
            }
        }
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
        public double Elo => LSR * 173.72 + 1500;

        public Player(string name)
        {
            this.name = name;
        }

        public double ExpectedResult(Player opponent) =>
            Q / (Q + opponent.Q);

        public double GetLikelihoodLSR(History history, double lsr)
        {
            var losers = from Match match in history
                         where match.winner == this
                         select match.loser;

            var winners = from Match match in history
                          where match.loser == this
                          select match.winner;

            double q = Math.Exp(lsr);
            double π = 1.0;

            foreach (var loser in losers)
                π *= q / (q + loser.Q);
            foreach (var winner in winners)
                π *= winner.Q / (q + winner.Q);

            return π;
        }
        public double GetLikelihoodQ(History history, double q)
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

        public double EstimateScore(History history, double dx = 0.001)
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
