using ColossalFramework.UI;
using System.Linq;
using UnityEngine;

namespace CSL_SimpleMetrics.Helpers
{
    public class GameObjectHelper
    {
        public static UIView GetUIViewGameObject()
        {
            var allUiViews = GameObject.FindObjectsOfType<UIView>();
            var mainUiView = allUiViews.Where(v => v.name == "UIView").FirstOrDefault();
            return mainUiView;
        }
    }
}
