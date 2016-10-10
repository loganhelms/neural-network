using System;

namespace NeuralNetwork
{
    public class LinearFunction : ITransferFunction
    {
        private static readonly Lazy<LinearFunction> Lazy = new Lazy<LinearFunction>(() => new LinearFunction());
        public static LinearFunction Instance => Lazy.Value;

        private LinearFunction() { }

        public double Evaluate(double x)
        {
                return x;
        }

        public double EvaluateDerivative(double x)
        {
            return 1.0;
        }
    }
}
