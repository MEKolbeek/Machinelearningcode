using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ModelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictController : ControllerBase
    {
        private static string modelPath = @"C:\Users\mkolb\Documents\LightGBMmodel.zip";
        private static MLContext mlContext = new MLContext();
        private static ITransformer model;

        static PredictController()
        {
            model = mlContext.Model.Load(modelPath, out _);
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] HandGestureData data)
        {
            string jsonData = LogHandGestureData(data);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<HandGestureData, HandGesturePrediction>(model);
            var prediction = predictionEngine.Predict(data);

            int predictedGestureNumber = (int)prediction.Label;

            Debug.WriteLine($"Voorspelde handgebaar nummer: {predictedGestureNumber}");

            return Ok(new { jsonData = jsonData });
        }

        private string LogHandGestureData(HandGestureData data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            Debug.WriteLine(jsonData);
            return jsonData;
        }
    }
}
