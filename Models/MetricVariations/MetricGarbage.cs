namespace CSL_SimpleMetrics.Models.MetricsVariations
{
    public class MetricGarbage : Metric
    {
        public MetricGarbage()
        {
            base.ApplyPesimisticConsumption = false;
        }

        public override float GetRatio()
        {
            float ratio = base.GetRatio();
            return 2 - ratio;
        }
    }
}
