using System;
using Xunit;
using NeuralNetwork;

namespace NeuralNetwork.UnitTests
{
    public class LinearFunction_EvaluateDerivativeShould
    {
        private readonly LinearFunction linearFunction;

        public LinearFunction_EvaluateDerivativeShould()
        {
            linearFunction = LinearFunction.Instance;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void ReturnOneGivenAnyValue(double value)
        {
            var result = linearFunction.EvaluateDerivative(value);

            Assert.Equal(result, 1);
        }
    }
}
