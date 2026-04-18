
namespace CSL_SimpleMetrics.Models
{
    public class Metric
    {
        public float Capacity { get; set; } = 0;
        public float Consumption { get; set; } = 0;
        public bool IsFlipped { get; set; } = false;
        public float Ratio => GetRatio();
        private float RatioMultiplier { get; set; } = 1;

        public float GetRatio()
        {
            float ratio = Capacity != 0 ? (Capacity / Consumption) * RatioMultiplier : 0;

            if (!IsFlipped)
            {
                return ratio;
            }

            return 2 - ratio;
        }

        public void SetRatioMultiplierDecimalPoints(int decimalPoints)
        {
            RatioMultiplier = (float)System.Math.Pow(10, decimalPoints);
        }

        public override string ToString()
        {
            return $"Capacity: {Capacity:0.0}, Consumption: {Consumption:0.0}, Ratio: {Ratio:0.00}";
        }
    }
}
