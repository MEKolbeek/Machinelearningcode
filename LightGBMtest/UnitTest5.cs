using Microsoft.ML;
using Microsoft.ML.Trainers.LightGbm;
using NUnit.Framework;
using System;
using System.IO;

namespace LightGBM.Test
{
    [TestFixture]
    public class EvaluationTest
    {
        [Test]
        public void TestModelTraining()
        {
            // Arrange
            var mlContext = new MLContext();
            var textLoader = mlContext.Data.CreateTextLoader<HandGestureData>(separatorChar: ';', allowQuoting: true, hasHeader: true);
            var dataview = textLoader.Load("C:/Users/mkolb/Documents/DatasetML.csv");
            var split = mlContext.Data.TrainTestSplit(dataview, testFraction: 0.3);
            var trainData = split.TrainSet;
            var testData = split.TestSet;

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Concatenate("Features", nameof(HandGestureData.Features)))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.MulticlassClassification.Trainers.LightGbm(new LightGbmMulticlassTrainer.Options()
                {
                    NumberOfLeaves = 8,
                    MinimumExampleCountPerLeaf = 25,
                    LearningRate = 0.1,
                    NumberOfIterations = 50
                }));

            // Act
            var model = pipeline.Fit(trainData);
            var metrics = mlContext.MulticlassClassification.Evaluate(model.Transform(testData));

            // Assert
            Assert.NotNull(model);
            Assert.IsTrue(metrics.MacroAccuracy >= 90 && metrics.MacroAccuracy <= 99.999999);
            Assert.IsTrue(metrics.MicroAccuracy >= 90 && metrics.MicroAccuracy <= 99.999999);
            Assert.NotNull(metrics.ConfusionMatrix);
            Assert.IsTrue(File.Exists("metrics.txt"));
        }
    }
}