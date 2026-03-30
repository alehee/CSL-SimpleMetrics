
using System.Collections.Generic;

namespace CSL_SimpleMetrics.Models
{
    class MetricsCombined
    {
        private Dictionary<MetricsEnum, Metric> Metrics { get; }

        public MetricsCombined()
        {
            Metrics = new Dictionary<MetricsEnum, Metric>();

            foreach (MetricsEnum metric in System.Enum.GetValues(typeof(MetricsEnum)))
            {
                Metrics[metric] = new Metric();
            }
        }

        public Metric Get(MetricsEnum key) => Metrics.TryGetValue(key, out var m) ? m : null;
        public void Set(MetricsEnum key, Metric value) => Metrics[key] = value;
        public List<MetricsEnum> Keys => new List<MetricsEnum>(Metrics.Keys);
    }
}
