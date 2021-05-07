using System;

namespace LSRE2
{
    internal static class Calculus
    {
        public static double Integral01(Func<double, double> f, double dx)
        {
            double Σ = 0.0;
            for (double x = 0.0; x < 1.0; x += dx)
                Σ += f(x) * dx;
            return Σ;
        }

        public static double Average01(Func<double, double> f, double dx) =>
            Integral01((x) => x * f(x), dx) / Integral01(f, dx);
    }
}
