using System;
using Xunit;
using NeuralNetwork;

namespace NeuralNetwork.UnitTests
{
    public class Weight_UpdateShould
    {
        private readonly Weight weight;

        public Weight_UpdateShould()
        {
            weight = new Weight();
        }

        [Fact]
        public void AssignWeightDeltaOneGivenAllParametersAreOne()
        {
            // arrange -
            // weight.Value is a random doulbe between -0.5 and 0.5 excluding 0.0 assigned in the constructor
            // we set it to 0 here only to test the Update method
            weight.Value = 0;

            // act
            weight.Update(1, 1, 1);
            var weightDeltaResult = weight.WeightDelta;
            var valueResult = weight.Value;

            // assert
            Assert.Equal(weightDeltaResult, 1);
            Assert.Equal(valueResult, 1);
        }
    }
}
