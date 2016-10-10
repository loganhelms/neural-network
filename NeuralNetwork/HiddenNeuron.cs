namespace NeuralNetwork
{
    public class HiddenNeuron : Neuron
    {
        public HiddenNeuron(ITransferFunction tFunc) : base(tFunc)
        {
            Bias = new Bias();
        }

        public sealed override void CalculateOutput()
        {
            Output += Bias.Calculate();
            Output = base.TransferFunction.Evaluate(Output);
        }

        public sealed override void AddToErrorSignalSum(double weight, double errorSignal)
        {
            ErrorSignalSum += weight * errorSignal;
        }

        public sealed override void CalculateErrorSignal()
        {
            ErrorSignal = base.TransferFunction.EvaluateDerivative(Output) * ErrorSignalSum;
        }
    }
}
