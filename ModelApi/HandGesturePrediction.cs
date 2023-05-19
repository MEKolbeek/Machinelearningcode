using Microsoft.ML.Data;

namespace ModelApi
{
    public class HandGesturePrediction
    {
        [ColumnName("PredictedLabel")]
        public int PredictedGestureNumber;

        public float[] Score;
    }
}
