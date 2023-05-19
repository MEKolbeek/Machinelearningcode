using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Newtonsoft.Json;

namespace ModelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictController : ControllerBase
    {
        private static string modelPath = @"D:\Programming\C#\Machinelearningcode\DatasetML\LightGBMmodel.zip"; 
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
            return Ok(new { jsonData = jsonData, /*prediction = prediction.PredictedGestureNumber*/ });
        }



        private string LogHandGestureData(HandGestureNumber data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            Console.WriteLine(jsonData);
            return jsonData;

        }
    }
}
