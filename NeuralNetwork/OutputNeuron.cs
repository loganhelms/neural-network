using System;

namespace NeuralNetwork
{
    public class OutputNeuron : Neuron
    {
        public OutputNeuron(ITransferFunction tFunc) : base(tFunc)
        {
            Bias = new Bias();
        }

        public sealed override void CalculateOutput()
        {
            Output += Bias.Calculate();
            Output = base.TransferFunction.Evaluate(Output);
        }

        public sealed override void CalculateErrorSignal(double target)
        {
            SquaredError = Math.Pow((target - Output), 2);
            ErrorSignal = (target - Output) * base.TransferFunction.EvaluateDerivative(Output);
        }
    }
}
