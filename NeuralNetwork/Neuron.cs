namespace NeuralNetwork
{
    public abstract class Neuron
    {
        public ITransferFunction TransferFunction { get; set; }
        public double Output { get; set; }
        public double SquaredError { get; set; }
        public double ErrorSignal { get; set; }
        public double ErrorSignalSum { get; set; }
        public Bias Bias { get; set; }

        protected Neuron(ITransferFunction tFunc)
        {
            TransferFunction = tFunc;
            Output = 0.0;
            ErrorSignal = 0.0;
            ErrorSignalSum = 0.0;
        }

        public virtual void AddToSum(double input, double weight)
        {
            Output += input * weight;
        }

        public virtual void AddToErrorSignalSum(double weight, double errorSignal) { }
        public virtual void CalculateOutput() { }
        public virtual void CalculateErrorSignal() { }
        public virtual void CalculateErrorSignal(double target) { }
    }
}
