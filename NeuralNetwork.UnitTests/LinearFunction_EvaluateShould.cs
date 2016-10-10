using System;
using Xunit;
using NeuralNetwork;

namespace NeuralNetwork.UnitTests
{
    public class LinearFunction_EvaluateShould
    {
        private readonly LinearFunction linearFunction;

        public LinearFunction_EvaluateShould()
        {
            linearFunction = LinearFunction.Instance;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void ReturnTheSameValue(double value)
        {
            var result = linearFunction.Evaluate(value);

            Assert.Equal(result, value);
        }
    }
}
