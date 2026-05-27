namespace CSL_SimpleMetrics.Models.MetricVariations
{
    public class MetricGarbage : Metric
    {
        public MetricGarbage()
        {
            base.ApplyPesimisticConsumption = false;
        }

        public override float GetRatio()
        {
            return Capacity != 0 ? 1 - (Consumption / Capacity) : 1;
        }
    }
}
