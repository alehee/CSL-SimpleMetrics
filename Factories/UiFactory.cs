using ColossalFramework.UI;
using UnityEngine;

namespace CSL_SimpleMetrics.Factories
{
    public class UIFactory
    {
        private Transform _parent;
        public UIFactory(Transform parent)
        {
            _parent = parent;
        }

        public UILabel CreateLabel(string name)
        {
            var gameObject = new GameObject($"{name}");
            gameObject.transform.parent = _parent;
            UILabel label = gameObject.AddComponent<UILabel>();
            return label;
        }
    }
}
