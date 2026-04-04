using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Logging;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Factories
{
    public class UiFactory
    {
        private Transform _parent;
        public UiFactory(Transform parent)
        {
            _parent = parent;
        }

        public UILabel CreateLabel(string name)
        {
            // TODO validate
            var gameObject = new GameObject($"{AppInformation.AppPrefix}_{name}");
            Logger.Log($"Parent: {_parent.name}"); // debug
            gameObject.transform.parent = _parent;
            UILabel label = gameObject.AddComponent<UILabel>();
            return label;
        }
    }
}
