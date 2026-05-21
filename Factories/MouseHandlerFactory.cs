using ColossalFramework.UI;
using CSL_SimpleMetrics.Helpers;
using CSL_SimpleMetrics.Logging;
using CSL_SimpleMetrics.Models;
using System.Collections;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Factories
{
    public class MouseHandlerFactory
    {
        private UIView _uiView = null;
        private UITabstrip _mainTabstrip = null;

        const int maxFramesToWaitForUI = 60;

        public MouseHandlerFactory()
        {
            GetUIComponents();
        }

        private void GetUIComponents()
        {
            if (_uiView == null)
            {
                _uiView = GameObjectHelper.GetUIViewGameObject().GetComponent<UIView>();
            }

            if (_mainTabstrip == null)
            {
                _mainTabstrip = GameObjectHelper.GetUIViewGameObject().FindUIComponent<UITabstrip>("MainToolstrip");
            }
        }

        public MouseEventHandler CreateMouseClickHandler(
            MetricsEnum metricEnum
        )
        {
            if (_mainTabstrip == null || _uiView == null)
                GetUIComponents();

            if (_mainTabstrip == null || _uiView == null)
                Logger.Log("Could not find all UI components to attach mouse click handler.", LogLevelEnum.Error);

            var metricIndex = GetTabstripMetricIndex(metricEnum);
            if (!metricIndex.HasValue)
                return null;

            return (component, eventParam) =>
            {
                _mainTabstrip.selectedIndex = metricIndex.Value;

                // Optionally switch tab in the panel
                _uiView.StartCoroutine(TrySwitchTab(metricEnum));
            };
        }

        private int? GetTabstripMetricIndex(MetricsEnum metricsEnum)
        {
            switch(metricsEnum)
            {
                case MetricsEnum.Electricity:
                    return 4;
                case MetricsEnum.Water:
                case MetricsEnum.Sewage:
                    return 5;
                case MetricsEnum.Garbage:
                case MetricsEnum.GarbageIncineration:
                    return 6;
                case MetricsEnum.Healthcare:
                case MetricsEnum.ChildCare:
                case MetricsEnum.SeniorCare:
                case MetricsEnum.Crematorium:
                case MetricsEnum.Cemetery:
                    return 8;
                case MetricsEnum.EducationElementary:   // Additional tab change needed
                case MetricsEnum.EducationHighSchool:   // Additional tab change needed
                case MetricsEnum.EducationUniversity:   // Additional tab change needed
                case MetricsEnum.Library:               // Additional tab change needed
                    return 12;
                case MetricsEnum.Police:
                    return 10;
                case MetricsEnum.FireSafety:
                    return 9;
                default:
                    Logger.Log($"No metric index found for {metricsEnum}. Defaulting to null.", LogLevelEnum.Warning);
                    return null;
            }
        }

        private IEnumerator TrySwitchTab(MetricsEnum metricsEnum)
        {
            yield return null;

            switch(metricsEnum)
            {
                case MetricsEnum.Healthcare:
                case MetricsEnum.ChildCare:
                case MetricsEnum.SeniorCare:
                case MetricsEnum.Crematorium:
                case MetricsEnum.Cemetery:
                    yield return WaitAndSwitchTabInHealthcarePanel(metricsEnum);
                    break;
                case MetricsEnum.EducationElementary:
                case MetricsEnum.EducationHighSchool:
                case MetricsEnum.EducationUniversity:
                case MetricsEnum.Library:
                    yield return WaitAndSwitchTabInEducationPanel(metricsEnum);
                    break;
            }
        }

        private IEnumerator WaitAndSwitchTabInHealthcarePanel(MetricsEnum metricsEnum)
        {
            const string panelName = "(Library) HealthInfoViewPanel";

            for (int i = 0; i < maxFramesToWaitForUI; i++)
            {
                var uiTabStrip = GameObjectHelper.FindUIComponent<UITabstrip>(panelName, "Tabstrip");

                if (uiTabStrip == null || !uiTabStrip.isVisible)
                {
                    yield return null;
                    continue;
                }

                switch (metricsEnum)
                {
                    case MetricsEnum.Healthcare:
                        uiTabStrip.selectedIndex = 0;
                        yield break;
                    case MetricsEnum.ChildCare:
                        uiTabStrip.selectedIndex = 2;
                        yield break;
                    case MetricsEnum.SeniorCare:
                        uiTabStrip.selectedIndex = 3;
                        yield break;
                    case MetricsEnum.Crematorium:
                    case MetricsEnum.Cemetery:
                        uiTabStrip.selectedIndex = 1;
                        yield break;
                }
                yield return null;
            }
        }

        private IEnumerator WaitAndSwitchTabInEducationPanel(MetricsEnum metricsEnum)
        {
            const string panelName = "(Library) EducationInfoViewPanel";

            for (int i = 0; i < maxFramesToWaitForUI; i++)
            {
                var uiTabStrip = GameObjectHelper.FindUIComponent<UITabstrip>(panelName, "Tabstrip");

                if (uiTabStrip == null || !uiTabStrip.isVisible)
                {
                    yield return null;
                    continue;
                }

                switch (metricsEnum)
                {
                    case MetricsEnum.EducationElementary:
                        uiTabStrip.selectedIndex = 0;
                        yield break;
                    case MetricsEnum.EducationHighSchool:
                        uiTabStrip.selectedIndex = 1;
                        yield break;
                    case MetricsEnum.EducationUniversity:
                        uiTabStrip.selectedIndex = 2;
                        yield break;
                    case MetricsEnum.Library:
                        uiTabStrip.selectedIndex = 4;
                        yield break;
                }
                yield return null;
            }
        }
    }
}
