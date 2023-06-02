using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace SVMClustering
{
    // Definieer de structuur van de dataset
    public class HandGestureData
    {
        // Eigenschap die de kenmerken (features) bevat
        [LoadColumn(0, 62), VectorType(63)]
        public float[] Features { get; set; }

        // Eigenschap die de label bevat
        [LoadColumn(63), ColumnName("Label")]
        public float Label { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Maak een MLContext object
            var mlContext = new MLContext();

            // Laad de dataset zonder kolomnamen
            var textLoader = mlContext.Data.CreateTextLoader<HandGestureData>(separatorChar: ';', allowQuoting: true, hasHeader: true);
            var dataview = textLoader.Load(@"C:/Users/mkolb/Documents/DatasetML.csv");

            // Split de dataset in een trainingsset en een testset
            var split = mlContext.Data.TrainTestSplit(dataview, testFraction: 0.3);
            var trainData = split.TrainSet;
            var testData = split.TestSet;

            // Definieer de pipeline van het model
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Concatenate("Features", nameof(HandGestureData.Features)))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("Label"))
                .Append(mlContext.BinaryClassification.Trainers.LinearSvm());

            // Train het model
            var model = pipeline.Fit(trainData);

            // Transformeer de trainingsdata met het getrainde model
            var transformedTrainData = model.Transform(trainData);

            // Transformeer de testdata met het getrainde model
            var transformedTestData = model.Transform(testData);

            // Evalueer het model op de testset
            var metrics = mlContext.BinaryClassification.Evaluate(transformedTestData);

            // Schrijf de evaluatiemetrics naar een tekstbestand
            using (var writer = new StreamWriter("metrics.txt"))
            {
                writer.WriteLine($"Accuracy: {metrics.Accuracy}");
                writer.WriteLine($"AreaUnderRocCurve: {metrics.AreaUnderRocCurve}");
                writer.WriteLine($"F1Score: {metrics.F1Score}");
            }

            // Open het bestand met de standaard teksteditor
            Process.Start("notepad.exe", "metrics.txt");

        }
    }
}
