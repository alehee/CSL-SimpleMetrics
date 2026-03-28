using ColossalFramework;
using CSL_SimpleMetrics.Models;

namespace CSL_SimpleMetrics.Services
{
    public sealed class MetricsService
    {
        private static MetricsService _instance;

        private DistrictManager _districtManager;
        private Metric ElectricityMetric;

        private MetricsService()
        {
            _districtManager = Singleton<DistrictManager>.instance;

            // Adjust to all metrics and remove hardcoded values
            ElectricityMetric = new Metric
            {
                    Capacity = 12.4f,
                    Consumption = 8.7f
            };
        }

        public static MetricsService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MetricsService();
            }
            return _instance;
        }

        public void UpdateCapacityAndConsumption()
        {
            District district = _districtManager.m_districts.m_buffer[0];

            // Example: Get electricity
            ElectricityMetric = new Metric
            {
                Capacity = district.GetElectricityCapacity(),
                Consumption = district.GetElectricityConsumption()
            };
        }

        public string GetElectricityMetricString()
        {
            return $"Electricity - Capacity: {ElectricityMetric.Capacity:0.0}, Consumption: {ElectricityMetric.Consumption:0.0}, Ratio: {ElectricityMetric.Ratio:0.00}";
        }
    }
}
