using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using NUnit.Framework;

namespace LightGBM.Tests
{
    [TestFixture]
    public class DatasetTests
    {
        private MLContext mlContext;

        [SetUp]
        public void Setup()
        {
            mlContext = new MLContext();
        }

[Test]
public void TestDatasetLoading()
{
    // Arrange
    var data = new[]
    {
        new YourDataClass { Feature1 = 1.0f, Feature2 = 2.0f, Label = 3.0f },
        new YourDataClass { Feature1 = 4.0f, Feature2 = 5.0f, Label = 6.0f },
        new YourDataClass { Feature1 = 7.0f, Feature2 = 8.0f, Label = 9.0f }
    };

    var dataView = mlContext.Data.LoadFromEnumerable(data);

    // Act
    var rowCount = dataView.Preview().RowView.Length;

    // Assert
    Assert.NotNull(dataView, "Dataset mag niet null zijn.");
    Assert.GreaterOrEqual(rowCount, 3, "De geladen dataset moet ten minste het verwachte aantal rijen hebben.");

    // Uitvoer en controle van gegevens
    var preview = dataView.Preview();
    Console.WriteLine($"Aantal kolommen in de dataset: {preview.ColumnView.Length}");
    foreach (var column in preview.ColumnView)
    {
        Console.WriteLine($"Kolomnaam: {column.Column.Name}, Index: {column.Column.Index}");
    }
}
    }
    

    // Definieer de structuur van de dataset
    public class YourDataClass
    {
        [LoadColumn(0)]
        public float Feature1 { get; set; }

        [LoadColumn(1)]
        public float Feature2 { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }
    }
}
