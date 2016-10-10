namespace NeuralNetwork
{
    public class Bias
    {
        public double Value { get; set; }
        public Weight Weight { get; set; }

        public Bias(double value = 1.0)
        {
            Value = value;
            Weight = new Weight();
        }

        public double Calculate()
        {
            return Value * Weight.Value;
        }
    }
}
