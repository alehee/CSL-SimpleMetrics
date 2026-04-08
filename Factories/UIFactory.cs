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

        public UILabel CreateLabel(
            string name, 
            string text, 
            float verticalMargin = 0, 
            WindowZOrderEnum zOrderEnum = WindowZOrderEnum.Content
        )
        {
            var gameObject = new GameObject($"{name}");
            gameObject.transform.parent = _parent;
            gameObject.transform.localPosition = new Vector3(0, verticalMargin);
            UILabel label = gameObject.AddComponent<UILabel>();
            label.text = text;
            label.textScale = 0.5f;
            label.zOrder = (int)zOrderEnum;
            return label;
        }
    }
}
