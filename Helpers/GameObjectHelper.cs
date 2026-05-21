using ColossalFramework.UI;
using CSL_SimpleMetrics.Logging;
using System.Linq;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

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

        public static T FindUIComponent<T>(string gameObjectParent, string gameObjectChild) where T : UIComponent
        {
            var child = GameObject.Find(gameObjectParent).transform.Find(gameObjectChild);
            if (child == null)
            {
                Logger.Log($"Could not find child {gameObjectChild} in parent {gameObjectParent}.", LogLevelEnum.Error);
                return null;
            }
            return child.GetComponent<T>();
        }
    }
}
