using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML.Transforms;

namespace LightGBM
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
            var dataview = textLoader.Load(@"D:\\Programming\\C#\\Machinelearningcode\\DatasetML\\DatasetML.csv");

            // Split de dataset in een trainingsset en een testset
            var split = mlContext.Data.TrainTestSplit(dataview, testFraction: 0.3);
            var trainData = split.TrainSet;
            var testData = split.TestSet;

          
            // Definieer de pipeline van het model
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                // De kolom "Label" wordt geconverteerd naar een numerieke sleutel (key)
                // Dit is vereist voor multiclass classificatie
                .Append(mlContext.Transforms.Concatenate("Features", nameof(HandGestureData.Features))) //niet noodzakelijk
                // Combineer de kolommen met de functies in een enkele kolom genaamd "Features"
                // Dit is vereist omdat LightGBM alleen werkt met een enkele kolom voor de functies
                .Append(mlContext.Transforms.NormalizeMinMax("Features")) //niet noodzakelijk
                // Normaliseer de waarden in de "Features" kolom zodat ze binnen een bepaalde range liggen
                // Dit helpt de algoritmes beter te presteren

                .Append(mlContext.MulticlassClassification.Trainers.LightGbm(new LightGbmMulticlassTrainer.Options()
                {
                    NumberOfLeaves = 8,
                    MinimumExampleCountPerLeaf = 25,
                    LearningRate = 0.05,
                    NumberOfIterations = 50
                }));
            // Gebruik de LightGBM multiclass trainer met een aantal specifieke parameters om het model te trainen
            

            // Train het model
            var model = pipeline.Fit(trainData);

            // Evalueer het model op de testset
            var metrics = mlContext.MulticlassClassification.Evaluate(model.Transform(testData));

            // Print de nauwkeurigheid van het model
            Console.WriteLine($"Micro Accuracy: {metrics.MicroAccuracy}, Macro Accuracy: {metrics.MacroAccuracy}");
            Console.WriteLine($"Confusion Table: {Environment.NewLine}{metrics.ConfusionMatrix.GetFormattedConfusionTable()}");

            // Schrijf de metrics naar een tekstbestand
            using (var writer = new StreamWriter("metrics.txt"))
            {
                writer.WriteLine($"Micro Accuracy: {metrics.MicroAccuracy}, Macro Accuracy: {metrics.MacroAccuracy}");
                writer.WriteLine($"Confusion Table: {Environment.NewLine}{metrics.ConfusionMatrix.GetFormattedConfusionTable()}");
            }

            // Open het bestand met de standaard teksteditor
            Process.Start("notepad.exe", "metrics.txt");

            // Sla het model op als ZIP-bestand
            mlContext.Model.Save(model, trainData.Schema, @"D:\Programming\C#\Machinelearningcode\DatasetML\LightGBMmodel.zip");

        }
    }
}

