using System;
using Xunit;
using NeuralNetwork;

namespace NeuralNetwork.UnitTests
{
    public class SigmoidFunction_EvaluateDerivativeShould
    {
        private readonly SigmoidFunction sigmoidFunction;

        public SigmoidFunction_EvaluateDerivativeShould()
        {
            sigmoidFunction = SigmoidFunction.Instance;
        }

        [Fact]
        public void ReturnNegativeThirtyGivenNegativeFive()
        {
            var result = sigmoidFunction.EvaluateDerivative(-5);

            Assert.Equal(result, -30);
        }

        [Fact]
        public void ReturnZeroGivenZero()
        {
            var result = sigmoidFunction.EvaluateDerivative(0);

            Assert.Equal(result, 0);
        }

        [Fact]
        public void ReturnNegativeTwentyGivenFive()
        {
            var result = sigmoidFunction.EvaluateDerivative(5);

            Assert.Equal(result, -20);
        }
    }
}
