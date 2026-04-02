
namespace CSL_SimpleMetrics.Models
{
    public class Metric
    {
        public float Capacity { get; set; } = 0;
        public float Consumption { get; set; } = 0;
        public float Ratio => Capacity != 0 ? (Capacity / Consumption) * RatioMultiplier : 0;
        private float RatioMultiplier { get; set; } = 1;

        public void SetRatioMultiplierDecimalPoints(int decimalPoints)
        {
            RatioMultiplier = (float)System.Math.Pow(10, decimalPoints);
        }
    }
}
