namespace CSL_SimpleMetrics.Models.MetricVariations
{
    public class MetricLibrary : Metric
    {
        public MetricLibrary()
        {
            base.ApplyPesimisticConsumption = false;
        }
    
        public override float GetRatio()
        {
            return base.Consumption / base.Capacity;
        }
    }
}
