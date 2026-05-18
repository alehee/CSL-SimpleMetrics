namespace CSL_SimpleMetrics.Models.MetricVariations
{
    public class MetricFireSafety : Metric
    {
        public MetricFireSafety()
        {
            base.ApplyPesimisticConsumption = false;
        }

        public override float GetRatio()
        {
            return (base.Consumption - base.Capacity) * 0.01f;
        }
    }
}
