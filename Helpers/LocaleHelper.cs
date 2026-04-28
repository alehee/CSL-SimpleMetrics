using ColossalFramework.Globalization;
using CSL_SimpleMetrics.Logging;
using CSL_SimpleMetrics.Models;
using System;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Helpers
{
    public static class LocaleHelper
    {
        public static string GetMetricsLocaleString(MetricsEnum key)
        {
            try
            {
                return Locale.Get(GetMetricsLocaleKeyFromTemplate(key));
            }
            catch (Exception ex)
            {
                Logger.Log($"Error retrieving locale string for key '{key}': {ex.Message}", LogLevelEnum.Warning);
                return $"[{key.ToString()}]";
            }
        }

        private static string GetMetricsLocaleKeyFromTemplate(MetricsEnum key)
        {
            switch (key)
            {
                case MetricsEnum.Electricity:
                case MetricsEnum.Water:
                case MetricsEnum.Heating:
                case MetricsEnum.Garbage:
                    return $"INFO_{key.ToString().ToUpper()}_TITLE";
                case MetricsEnum.Sewage:
                    return "INFO_WATER_SEWAGEAVAILABILITY";
                case MetricsEnum.GarbageIncineration:
                    return "INFO_GARBAGE_INCINERATOR";
                case MetricsEnum.Healthcare:
                case MetricsEnum.ChildCare:
                    return $"INFO_HEALTH_{key.ToString().ToUpper()}";
                case MetricsEnum.SeniorCare:
                    return "INFO_HEALTH_ELDERCARE";
                case MetricsEnum.Cemetery:
                    return "INFO_HEALTH_CEMETARYUSAGE";
                case MetricsEnum.Crematorium:
                    return "INFO_HEALTH_CREMATORIUMAVAILABILITY";
                case MetricsEnum.EducationElementary:
                case MetricsEnum.EducationHighSchool:
                case MetricsEnum.EducationUniversity:
                case MetricsEnum.Library:
                    string subkey = key.ToString().Replace("Education", "").Replace("School", "").ToUpper();
                    return $"INFO_EDUCATION_{subkey}";
                case MetricsEnum.FireSafety:
                    return "INFO_FIRE_TITLE";
                case MetricsEnum.Police:
                    return "INFO_CRIMERATE_TITLE";
                default:
                    throw new ArgumentException($"Unsupported MetricsEnum key: {key}");
            }
        }
    }
}
