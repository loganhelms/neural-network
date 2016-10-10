using System;

namespace NeuralNetwork
{
    public class Weight
    {
        public double Value { get; set; }
        public double WeightDelta { get; set; }

        private static Random gen = new Random();

        public Weight()
        {
            double temp = 0.0;
            do
            {
                temp = gen.NextDouble() - 0.5;
            } while (temp == 0.0);

            Value = temp;
        }

        public void Update(double learningRate, double errorSignal, double output)
        {
            WeightDelta = learningRate * errorSignal * output;
            Value += WeightDelta;
        }
    }
}
