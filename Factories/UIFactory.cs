using ColossalFramework.UI;
using CSL_SimpleMetrics.Models;
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

        public UILabel CreateLabel(string name, string text, WindowZOrderEnum zOrderEnum = WindowZOrderEnum.Content)
        {
            var gameObject = new GameObject($"{name}");
            gameObject.transform.parent = _parent;
            gameObject.transform.localPosition = Vector3.zero;
            UILabel label = gameObject.AddComponent<UILabel>();
            label.text = text;
            label.zOrder = (int)zOrderEnum;
            return label;
        }
    }
}
