using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Behaviours
{
    public class Manager : MonoBehaviour
    {
        private float _timer;
        private float _interval = 2f; // Seconds

        public void Start()
        {

        }

        public void Update()
        {
            if (_timer >= _interval)
            {
                _timer = 0f;
                // Update behaviour
                Logger.Log("Updating behaviour...");
            }

            _timer += Time.deltaTime;
        }
    }
}
