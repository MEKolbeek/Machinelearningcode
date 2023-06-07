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
        public ActionResult<string> Post([FromBody] HandGestureNumber data)
        {
            string jsonData = LogHandGestureData(data);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<HandGestureNumber, HandGesturePrediction>(model);
            var prediction = predictionEngine.Predict(data);

            int predictedGestureNumber = prediction.cijfer; // Haal de voorspelde integer-waarde op uit de "cijfer" kolom

           Debug.WriteLine($"Voorspelde handgebaar nummer: {predictedGestureNumber}"); // Geef de waarde weer in de debug-output

            return Ok(new { jsonData = jsonData });
        }

                private string LogHandGestureData(HandGestureNumber data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            Debug.WriteLine(jsonData);
            return jsonData;

        }
    }
}
