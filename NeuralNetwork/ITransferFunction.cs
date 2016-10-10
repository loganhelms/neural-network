namespace NeuralNetwork
{
    public interface ITransferFunction
    {
        double Evaluate(double x);

        double EvaluateDerivative(double x);
    }
}
