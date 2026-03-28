
namespace CSL_SimpleMetrics.Models
{
    public class Metric
    {
        public float Capacity { get; set; } = 0;
        public float Consumption { get; set; } = 0;
        public float Ratio => Capacity != 0 ? Capacity / Consumption : 0;
    }
}
