using System;
using NeuralNetwork;

namespace NeuralNetwork.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create the samples that will be used to train the network.
            // With XOR, the training set and test set are the same.
            Sample[] samples = new Sample[4];
            samples[0] = new Sample { Inputs = new double[] { 0.1, 0.1 }, Targets = new double[] { 0.1 } };
            samples[1] = new Sample { Inputs = new double[] { 0.1, 0.9 }, Targets = new double[] { 0.9 } };
            samples[2] = new Sample { Inputs = new double[] { 0.9, 0.1 }, Targets = new double[] { 0.9 } };
            samples[3] = new Sample { Inputs = new double[] { 0.9, 0.9 }, Targets = new double[] { 0.1 } };

            // Create the layer sizes; 2 input layer nodes, 2 hidden layer nodes, 1 output node.
            // Note: It is possible to add more hidden layers if desired.
            // To add another hidden layer with 3 nodes in it might look like this: ushort[] layerSizes = {2, 2, 3, 1};
            ushort[] layerSizes = { 2, 2, 1 };

            // For each layer in the network assign a transfer function to be used by the neurons in that layer.
            // The input layer (first layer) must be null because input neurons do not have transfer functions.
            ITransferFunction[] tFuncs = { null, SigmoidFunction.Instance, SigmoidFunction.Instance };

            // The training rate of the backpropagation algorithm.
            double trainingRate = 0.001;

            Network net = new Network(layerSizes, tFuncs, trainingRate);

            do
            {
                System.Console.WriteLine("Training the network...");
                for (int i = 0; i < 1000000; i++)
                {
                    net.Train(samples);
                }
                System.Console.WriteLine("Results\n--------------");
                net.Test(samples);
                System.Console.WriteLine("\nContinue training the network with next 1,000,000 samples?(Y/N)");
            }
            while (System.Console.ReadLine().Equals("Y", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
