using ColossalFramework.UI;
using CSL_SimpleMetrics.Helpers;
using CSL_SimpleMetrics.Logging;
using CSL_SimpleMetrics.Models;
using System;

namespace CSL_SimpleMetrics.Factories
{
    public class MouseHandlerFactory
    {
        private UITabstrip _mainTabstrip = null;

        public MouseHandlerFactory()
        {
            Logger.Log("Initializing MouseHandlerFactory.");
            GetUITabstrip();
        }

        private void GetUITabstrip()
        {
            if (_mainTabstrip == null)
            {
                _mainTabstrip = GameObjectHelper.GetUIViewGameObject().FindUIComponent<UITabstrip>("MainToolstrip");
            }
        }

        public MouseEventHandler CreateMouseClickHandler(
            MetricsEnum metricEnum
        )
        {
            if (_mainTabstrip == null)
                GetUITabstrip();

            if (_mainTabstrip == null)
                Logger.Log("Could not find MainToolstrip UITabstrip to attach mouse click handler.", LogLevelEnum.Error);

            var metricIndex = GetTabstripMetricIndex(metricEnum);
            if (!metricIndex.HasValue)
                return null;

            return (component, eventParam) =>
            {
                _mainTabstrip.selectedIndex = metricIndex.Value;
                Logger.Log($"Selected {metricEnum} tabstrip index {metricIndex.Value}.");
            };
        }

        private int? GetTabstripMetricIndex(MetricsEnum metricsEnum)
        {
            // TODO: implement rest of the metrics
            switch(metricsEnum)
            {
                case MetricsEnum.Water:
                    return 5;
                default:
                    Logger.Log($"No metric index found for {metricsEnum}. Defaulting to null.", LogLevelEnum.Warning);
                    return null;
            }
        }
    }
}
