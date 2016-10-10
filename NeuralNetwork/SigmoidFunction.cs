using System;

namespace NeuralNetwork
{
    public class SigmoidFunction : ITransferFunction
    {
        private static readonly Lazy<SigmoidFunction> Lazy = new Lazy<SigmoidFunction>(() => new SigmoidFunction());
        public static SigmoidFunction Instance => Lazy.Value;
        private SigmoidFunction() { }

        public double Evaluate(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public double EvaluateDerivative(double x)
        {
            return x * (1 - x);
        }
    }
}
