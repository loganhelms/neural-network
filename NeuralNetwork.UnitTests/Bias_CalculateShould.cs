using System;
using Xunit;
using NeuralNetwork;

namespace NeuralNetwork.UnitTests
{
    public class Bias_CalculateShould
    {
        private readonly Bias bias;

        public Bias_CalculateShould()
        {
            bias = new Bias();
        }

        [Fact]
        public void ReturnTwoGivenWeightValueIsTwo()
        {
            bias.Weight.Value = 2;
            var result = bias.Calculate();

            Assert.Equal(result, 2);
        }
    }
}
