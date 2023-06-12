using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using LightGBM;

namespace LightGBM.Tests
{
    public class PipelineTest
    {
        [Test]
        public void TestPipeline()
        {
            // Arrange
            var mlContext = new MLContext();
            var dataview = mlContext.Data.LoadFromTextFile<HandGestureData>("C:/Users/mkolb/Documents/DatasetML.csv", separatorChar: ';', hasHeader: true);
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
            var transformedData = pipeline.Fit(dataview).Transform(dataview);
            var columnNames = transformedData.Schema.Select(column => column.Name).ToArray();
            var estimatorTypes = GetEstimatorTypes(pipeline);

            // Assert
            Assert.Contains("ValueToKeyMappingEstimator", estimatorTypes);
            Assert.Contains("ColumnConcatenatingEstimator", estimatorTypes);
            Assert.Contains("NormalizingEstimator", estimatorTypes);
            Assert.Contains("LightGbmMulticlassTrainer", estimatorTypes);
        }
        private static string[] GetEstimatorTypes<TLastTransformer>(EstimatorChain<TLastTransformer> pipeline)
            where TLastTransformer : class, ITransformer
        {
            var estimatorsField = typeof(EstimatorChain<TLastTransformer>)
                .GetField("_estimators", BindingFlags.Instance | BindingFlags.NonPublic);
            var estimators = (IEstimator<ITransformer>[])estimatorsField.GetValue(pipeline);
            var estimatorTypes = estimators.Select(estimator => estimator.GetType().Name).ToArray();
            return estimatorTypes;
        }
    }
}
