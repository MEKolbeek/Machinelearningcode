using Microsoft.ML.Data;

namespace ModelApi
{
    public class HandGesturePrediction
    {
        [ColumnName("cijfer")]
        public int cijfer;

        public float[] Score;
    }
}
