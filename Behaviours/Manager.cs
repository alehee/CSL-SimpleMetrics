using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Services;
using UnityEngine;

namespace CSL_SimpleMetrics.Behaviours
{
    public class Manager : MonoBehaviour
    {
        public static string Name => $"{AppInformation.AppPrefix}_Manager";

        private float _timer;
        private float _interval = 2f; // Seconds

        private MetricsService metricsService;

        public void Start()
        {
            metricsService = MetricsService.GetInstance();
        }

        public void Update()
        {
            if (_timer >= _interval)
            {
                _timer = 0f;

                metricsService.UpdateCapacityAndConsumption();

                // Testing method
                //metricsService.PrintMetrics();
            }

            _timer += Time.deltaTime;
        }
    }
}
