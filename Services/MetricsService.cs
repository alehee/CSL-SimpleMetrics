using System;
using ColossalFramework;
using CSL_SimpleMetrics.Models;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Services
{
    public sealed class MetricsService
    {
        private static MetricsService _instance;

        private DistrictManager _districtManager;
        private ImmaterialResourceManager _immaterialResourceManager;
        private MetricsCombined _metrics;

        public event Action MetricsUpdated;

        private MetricsService()
        {
            _districtManager = Singleton<DistrictManager>.instance;
            _immaterialResourceManager = Singleton<ImmaterialResourceManager>.instance;
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

        public MetricsCombined GetMetrics() => _metrics;

        public void UpdateCapacityAndConsumption()
        {
            foreach(MetricsEnum metricKey in _metrics.Keys)
            {
                Metric newMetric = GetCapacityAndConsumption(metricKey);
                _metrics.Set(metricKey, newMetric);
            }

            MetricsUpdated?.Invoke();
        }

        private Metric GetCapacityAndConsumption(MetricsEnum metricKey)
        {
            District district = _districtManager.m_districts.m_buffer[0];

            switch (metricKey)
            {
                case MetricsEnum.Electricity:
                    return new Metric
                    {
                        Capacity = district.GetElectricityCapacity(),
                        Consumption = district.GetElectricityConsumption()
                    };
                case MetricsEnum.Water:
                    return new Metric
                    {
                        Capacity = district.GetWaterCapacity(),
                        Consumption = district.GetWaterConsumption()
                    };
                case MetricsEnum.Heating:
                    return new Metric
                    {
                        Capacity = district.GetHealCapacity(),
                        Consumption = district.GetHeatingConsumption()
                    };
                case MetricsEnum.Sewage:
                    return new Metric
                    {
                        Capacity = district.GetSewageCapacity(),
                        Consumption = district.GetSewageAccumulation()
                    };
                case MetricsEnum.Garbage:
                    return new Metric
                    {
                        Capacity = district.GetGarbageCapacity(),
                        Consumption = district.GetGarbageAmount()
                    };
                case MetricsEnum.GarbageIncineration:
                    return new Metric
                    {
                        Capacity = district.GetIncinerationCapacity(),
                        Consumption = district.GetGarbageAccumulation()
                    };
                case MetricsEnum.Healthcare:
                    return new Metric
                    {
                        Capacity = district.GetHealCapacity(),
                        Consumption = district.GetSickCount()
                    };
                case MetricsEnum.ChildCare:
                    var metricChild = new Metric
                    {
                        Capacity = district.m_childHealthData.m_finalCount,
                        Consumption = district.m_childData.m_finalCount + district.m_teenData.m_finalCount
                    };
                    metricChild.SetRatioMultiplierDecimalPoints(-2);
                    return metricChild;
                case MetricsEnum.SeniorCare:
                    var metricSenior = new Metric
                    {
                        Capacity = district.m_seniorHealthData.m_finalCount,
                        Consumption = district.m_seniorData.m_finalCount
                    };
                    metricSenior.SetRatioMultiplierDecimalPoints(-2);
                    return metricSenior;
                case MetricsEnum.Crematorium:
                    return new Metric
                    {
                        Capacity = district.GetCremateCapacity(),
                        Consumption = district.GetDeadCount()
                    };
                case MetricsEnum.Cemetery:
                    return new Metric
                    {
                        Capacity = district.GetDeadCapacity(),
                        Consumption = district.GetDeadAmount()
                    };
                case MetricsEnum.EducationElementary:
                    return new Metric
                    {
                        Capacity = district.GetEducation1Capacity(),
                        Consumption = district.GetEducation1Need()
                    };
                case MetricsEnum.EducationHighSchool:
                    return new Metric
                    {
                        Capacity = district.GetEducation2Capacity(),
                        Consumption = district.GetEducation2Need()
                    };
                case MetricsEnum.EducationUniversity:
                    return new Metric
                    {
                        Capacity = district.GetEducation3Capacity(),
                        Consumption = district.GetEducation3Need()
                    };
                case MetricsEnum.Library:
                    return new Metric
                    {
                        Capacity = district.GetLibraryCapacity(),
                        Consumption = district.GetLibraryVisitorCount()
                    };
                case MetricsEnum.FireSafety:
                    int fireHazardValue = 0;
                    _immaterialResourceManager.CheckTotalResource(ImmaterialResourceManager.Resource.FireHazard, out fireHazardValue);
                    fireHazardValue = Mathf.Clamp(fireHazardValue, 0, 100);
                    return new Metric
                    {
                        Capacity = fireHazardValue,
                        Consumption = 100
                    };
                case MetricsEnum.Police:
                    return new Metric
                    {
                        Capacity = district.GetCriminalCapacity(),
                        Consumption = district.GetCriminalAmount() + district.GetExtraCriminals()
                    };
                default:
                    return null;
            }
        }

        // Testing method
        public void PrintMetrics()
        {
            // Print the metrics for testing purposes
            //foreach (MetricsEnum metricKey in new List<MetricsEnum> { MetricsEnum.FireSafety })

            foreach (MetricsEnum metricKey in _metrics.Keys)
            {
                Metric metric = _metrics.Get(metricKey);
                Logger.Log($"{metricKey} - Capacity: {metric.Capacity:0.0}, Consumption: {metric.Consumption:0.0}, Ratio: {metric.Ratio:0.00}");
            }
        }
    }
}
