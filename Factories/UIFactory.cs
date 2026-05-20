using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Models;
using UnityEngine;

namespace CSL_SimpleMetrics.Factories
{
    public class UIFactory
    {
        private Transform _parent;
        private UITextureAtlas _atlas;

        public UIFactory(Transform parent, UITextureAtlas atlas)
        {
            _parent = parent;
            _atlas = atlas;
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

        public UISprite CreateSprite(
            string name,
            float verticalMargin = -0.005f,
            float horizontalMargin = 0,
            float size = 1f,
            float opacity = 1f,
            WindowZOrderEnum zOrderEnum = WindowZOrderEnum.Content,
            Color? color = null
        )
        {
            var gameObject = new GameObject($"{ConfigurationConstants.AppPrefix}_Sprite_{name}");
            gameObject.transform.parent = _parent;
            gameObject.transform.localPosition = new Vector3(horizontalMargin, verticalMargin);
            gameObject.transform.localScale = new Vector3(size, size, 1);
            UISprite sprite = gameObject.AddComponent<UISprite>();
            sprite.atlas = _atlas;
            sprite.spriteName = name;
            sprite.zOrder = (int)zOrderEnum;

            if (color.HasValue)
                sprite.color = color.Value;

            sprite.opacity = opacity;

            return sprite;
        }
    }
}
