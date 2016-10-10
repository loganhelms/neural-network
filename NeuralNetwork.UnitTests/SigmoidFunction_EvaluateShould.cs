using System;
using Xunit;
using NeuralNetwork;

namespace NeuralNetwork.UnitTests
{
    public class SigmoidFunction_EvaluateShould
    {
        private readonly SigmoidFunction sigmoidFunction;

        public SigmoidFunction_EvaluateShould()
        {
            sigmoidFunction = SigmoidFunction.Instance;
        }

        [Fact]
        public void ReturnOneHalfGivenZero()
        {
            var result = sigmoidFunction.Evaluate(0);

            Assert.Equal(result, 0.5);
        }
    }
}
