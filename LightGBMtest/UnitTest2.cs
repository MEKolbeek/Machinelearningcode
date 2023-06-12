using System.Data;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using NUnit.Framework;
using LightGBM; 

namespace LightGBM.Tests
{
    [TestFixture]
    public class TestTrainSplittest
    {
        private MLContext mlContext;

        [SetUp]
        public void Setup()
        {
            mlContext = new MLContext(seed: 1);
        }

        [Test]
        public void TestDatasetSplitting()
        {
            // Arrange
            var dataPath = "C:/Users/mkolb/Documents/DatasetML.csv";
            var data = mlContext.Data.LoadFromTextFile<HandGestureData>(dataPath, separatorChar: ';', hasHeader: true);

            // Act
            var split = mlContext.Data.TrainTestSplit(data, testFraction: 0.3);
            var trainData = split.TrainSet;
            var testData = split.TestSet;

            // Assert
            Assert.NotNull(trainData, "Trainingsset mag niet null zijn.");
            Assert.NotNull(testData, "Testset mag niet null zijn.");

            var originalRowCount = data.GetColumn<float[]>(nameof(HandGestureData.Features)).Count();
            var trainRowCount = trainData.GetColumn<float[]>(nameof(HandGestureData.Features)).Count();
            var testRowCount = testData.GetColumn<float[]>(nameof(HandGestureData.Features)).Count();

            // Controleer of de trainingsset en testset niet leeg zijn
            Assert.Greater(trainRowCount, 0, "Trainingsset moet minstens één rij bevatten.");
            Assert.Greater(testRowCount, 0, "Testset moet minstens één rij bevatten.");

            // Controleer of het percentage van de trainingsset en testset overeenkomt met de verwachte waarden
            var expectedTrainPercentage = 0.7;
            var expectedTestPercentage = 0.3;

            var trainPercentage = (double)trainRowCount / originalRowCount;
            var testPercentage = (double)testRowCount / originalRowCount;

            Assert.AreEqual(expectedTrainPercentage, trainPercentage, 0.01, "Het percentage van de trainingsset komt niet overeen met het verwachte percentage.");
            Assert.AreEqual(expectedTestPercentage, testPercentage, 0.01, "Het percentage van de testset komt niet overeen met het verwachte percentage.");
        }
    }
}
