using ColossalFramework;
using CSL_SimpleMetrics.Logging;
using CSL_SimpleMetrics.Models;
using System.Collections.Generic;

namespace CSL_SimpleMetrics.Services
{
    public sealed class MetricsService
    {
        private static MetricsService _instance;

        private DistrictManager _districtManager;
        private MetricsCombined _metrics;

        private MetricsService()
        {
            _districtManager = Singleton<DistrictManager>.instance;
            _metrics = new MetricsCombined();
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

            foreach(MetricsEnum metricKey in _metrics.Keys)
            {
                Metric newMetric = GetCapacityAndConsumption(metricKey);
                _metrics.Set(metricKey, newMetric);
            }
        }

        private Metric GetCapacityAndConsumption(MetricsEnum metric)
        {
            switch (metric)
            {
                case MetricsEnum.Electricity:
                    return new Metric
                    {
                        Capacity = _districtManager.m_districts.m_buffer[0].GetElectricityCapacity(),
                        Consumption = _districtManager.m_districts.m_buffer[0].GetElectricityConsumption()
                    };
                case MetricsEnum.Water:
                    return new Metric
                    {
                        Capacity = _districtManager.m_districts.m_buffer[0].GetWaterCapacity(),
                        Consumption = _districtManager.m_districts.m_buffer[0].GetWaterConsumption()
                    };
                case MetricsEnum.Heating:
                    return new Metric
                    {
                        Capacity = _districtManager.m_districts.m_buffer[0].GetHealCapacity(),
                        Consumption = _districtManager.m_districts.m_buffer[0].GetHeatingConsumption()
                    };
                // TODO: add all cases
                default:
                    return null;
            }
        }

        // Testing property for electricity metric
        public void PrintMetrics()
        {
            foreach (MetricsEnum metricKey in new List<MetricsEnum>{ 
                MetricsEnum.Electricity, MetricsEnum.Water, MetricsEnum.Heating
            })
            {
                Metric metric = _metrics.Get(metricKey);
                Logger.Log($"{metricKey} - Capacity: {metric.Capacity:0.0}, Consumption: {metric.Consumption:0.0}, Ratio: {metric.Ratio:0.00}");
            }
        }
    }
}
