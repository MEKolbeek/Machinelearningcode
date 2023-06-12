using Microsoft.ML;
using Microsoft.ML.Trainers.LightGbm;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace LightGBM.Tests
{
    [TestFixture]
    public class ModelTrainingTest
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

            // Assert that the model is successfully trained without errors
            Assert.NotNull(metrics.MacroAccuracy);

            // Assert that the trained model has valid output and is not empty
            var predictions = model.Transform(testData);
            var predictionCount = mlContext.Data.CreateEnumerable<HandGestureData>(predictions, reuseRowObject: false).Count();
            Assert.IsTrue(predictionCount > 0);
        }
    }
}
