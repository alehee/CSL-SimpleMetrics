namespace CSL_SimpleMetrics.Models.MetricVariations
{
    public class MetricCemetery : Metric
    {
        public MetricCemetery()
        {
            base.ApplyPesimisticConsumption = false;
        }

        public override float GetRatio()
        {
            return 1 - base.Consumption / base.Capacity;
        }
    }
}
