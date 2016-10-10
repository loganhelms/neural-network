using System;

namespace NeuralNetwork
{
    public class Network
    {
        public Neuron[][] Neurons { get; set; }
        public Weight[][][] Weights { get; set; }
        public double LearningRate { get; set; }

        public Network(ushort[] layerSizes, ITransferFunction[] transferFunctions, double learningRate)
        {
            try
            {
                CreateNeurons(layerSizes, transferFunctions);
                CreateWeights(layerSizes);
                LearningRate = learningRate;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateNeurons(ushort[] layerSizes, ITransferFunction[] transferFunctions)
        {
            if (layerSizes.Length != transferFunctions.Length)
            {
                throw new ArgumentException("Cannot make a network with these parameters.");
            }

            if (transferFunctions[0] != null)
            {
                throw new ArgumentException("Input layer transfer function must be null.");
            }

            Neurons = new Neuron[layerSizes.Length][];
            for (byte l = 0; l < layerSizes.Length; l++)
            {
                // creates neurons in the input layer
                if (l == 0)
                {
                    Neurons[l] = new InputNeuron[layerSizes[l]];
                    for (ushort i = 0; i < layerSizes[l]; i++)
                    {
                        Neurons[l][i] = new InputNeuron(transferFunctions[l]);
                    }
                }

                // creates neurons in the hidden layer(s)
                else if (l < layerSizes.Length - 1)
                {
                    Neurons[l] = new HiddenNeuron[layerSizes[l]];
                    for (ushort i = 0; i < layerSizes[l]; i++)
                    {
                        Neurons[l][i] = new HiddenNeuron(transferFunctions[l]);
                    }
                }

                // creates neurons in the output layer
                else
                {
                    Neurons[l] = new OutputNeuron[layerSizes[l]];
                    for (ushort i = 0; i < layerSizes[l]; i++)
                    {
                        Neurons[l][i] = new OutputNeuron(transferFunctions[l]);
                    }
                }
            }
        }

        private void CreateWeights(ushort[] layerSizes )
        {
            // sets the number of layer weights
            Weights = new Weight[layerSizes.Length - 1][][];
            for (byte h = 1; h <= Weights.Length; h++)
            {
                // sets the number of weights in each layer
                Weights[h - 1] = new Weight[Neurons[h - 1].Length][];
                for (ushort i = 0; i < Neurons[h - 1].Length; i++)
                {
                    // sets the number of input weights
                    Weights[h - 1][i] = new Weight[Neurons[h].Length];
                    for (ushort j = 0; j < Neurons[h].Length; j++)
                    {
                        Weights[h - 1][i][j] = new Weight();
                    }
                }
            }
        }

        private void Propagate(double[] sample)
        {
            // load the input neurons from the sample
            for (ushort i = 0; i < Neurons[0].Length; i++)
            {
                Neurons[0][i].Output = sample[i];
            }

            // propagate through the hidden layer(s) to the output layer
            for (byte h = 1; h < Neurons.Length; h++)
            {
                for (ushort i = 0; i < Neurons[h].Length; i++)
                {
                    for (ushort j = 0; j < Neurons[h - 1].Length; j++)
                    {
                        Neurons[h][i].AddToSum(Neurons[h - 1][j].Output, Weights[h - 1][j][i].Value);
                    }
                    Neurons[h][i].CalculateOutput();
                }
            }
        }

        private void BackPropagate(double[] targets)
        {
            // Calculate error signals
            for (byte h = Convert.ToByte(Neurons.Length - 1); h > 0; h--)
            {
                for (ushort i = 0; i < Neurons[h].Length; i++)
                {
                    // output neuron(s)
                    if (Neurons[h][i].GetType() == typeof(OutputNeuron))
                    {
                        Neurons[h][i].CalculateErrorSignal(targets[i]);
                    }
                    // hidden neuron(s)
                    else if (Neurons[h][i].GetType() == typeof(HiddenNeuron))
                    {
                        for (ushort j = 0; j < Neurons[h + 1].Length; j++)
                        {
                            Neurons[h][i].AddToErrorSignalSum(Weights[h][i][j].Value, Neurons[h + 1][j].ErrorSignal);
                        }
                        Neurons[h][i].CalculateErrorSignal();
                    }
                }
            }
        }

        private void UpdateWeights()
        {
            for (byte h = 0; h < Weights.Length; h++)
            {
                for (ushort i = 0; i < Neurons[h].Length; i++)
                {
                    for (ushort j = 0; j < Neurons[h + 1].Length; j++)
                    {
                        Weights[h][i][j].Update(LearningRate, Neurons[h + 1][j].ErrorSignal, Neurons[h][i].Output);
                    }
                }
            }
        }

        private void UpdateBiasWeights()
        {
            for (byte h = 1; h < Neurons.Length; h++)
            {
                for (ushort i = 0; i < Neurons[h].Length; i++)
                {
                    Neurons[h][i].Bias.Weight.Update(LearningRate, Neurons[h][i].ErrorSignal, Neurons[h][i].Bias.Value);
                }
            }
        }

        private void ClearOutputsAndSums()
        {
            for (byte h = 1; h < Neurons.Length; h++)
            {
                for (ushort i = 0; i < Neurons[h].Length; i++)
                {
                    Neurons[h][i].Output = 0.0;
                    Neurons[h][i].ErrorSignalSum = 0.0;
                }
            }
        }

        public double MeanSquareError()
        {
            double error = 0.0;
            for (ushort i = 0; i < Neurons[Neurons.Length - 1].Length; i++)
            {
                error += Neurons[Neurons.Length - 1][i].SquaredError;
            }
            return 0.5 * error;
        }

        public void Train(Sample[] trainingSet)
        {
            for (int i = 0; i < trainingSet.Length; i++)
            {
                Propagate(trainingSet[i].Inputs);
                BackPropagate(trainingSet[i].Targets);
                UpdateWeights();
                UpdateBiasWeights();
                ClearOutputsAndSums();
            }
        }

        public void Test(Sample[] testSet)
        {
            int correctGuesses = 0;
            for (int i = 0; i < testSet.Length; i++)
            {
                Propagate(testSet[i].Inputs);
                Console.WriteLine( "{0}\t{1}\t{2}" , testSet[ i ].Inputs[ 0 ] , testSet[ i ].Inputs[ 1 ] , Neurons[ 2 ][ 0 ].Output );
                byte pattern = 0;
                byte output = 0;
                for (byte j = 1; j < testSet[i].Targets.Length; j++)
                {
                    if (testSet[i].Targets[j] > testSet[i].Targets[pattern])
                    {
                        pattern = j;
                    }
                    if (Neurons[Neurons.Length - 1][j].Output > Neurons[Neurons.Length - 1][output].Output)
                    {
                        output = j;
                    }
                }

                if (pattern == output)
                {
                    correctGuesses++;
                }
                ClearOutputsAndSums();
            }
            double result = (double)correctGuesses / testSet.Length;
        }
    }
}
