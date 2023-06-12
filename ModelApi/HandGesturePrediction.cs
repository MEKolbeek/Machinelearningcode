using Microsoft.ML.Data;

namespace ModelApi
{
    public class HandGesturePrediction
    {
        [ColumnName("Cijfer")]
        public int Label;

        public float[] Score;
    }
}
