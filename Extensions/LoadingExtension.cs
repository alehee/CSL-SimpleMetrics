using ColossalFramework.UI;
using CSL_SimpleMetrics.Behaviours;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.UI;
using ICities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Extensions
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private GameObject _modGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            _modGameObject = new GameObject(ConstConfiguration.AppPrefix);
            _modGameObject.transform.parent = GetUIViewGameObject().transform;
            _modGameObject.AddComponent<Manager>();
            _modGameObject.AddComponent<Window>();

            Logger.Log("Loaded moddification.");
        }

        public override void OnLevelUnloading()
        {
            var objectsToDestroy = new List<GameObject> {
                // If you are adding more game objects, add them here to be destroyed on unload.
                _modGameObject
            };

            for (int i = 0; i < objectsToDestroy.Count; i++)
            {
                if (objectsToDestroy[i] != null)
                {
                    Logger.Log($"Destroying game object: {objectsToDestroy[i].name}");
                    UnityEngine.Object.Destroy(objectsToDestroy[i].gameObject);
                }
            }

            Logger.Log("Unloaded moddification.");
        }

        private UIView GetUIViewGameObject()
        {
            var allUiViews = GameObject.FindObjectsOfType<UIView>();
            var mainUiView = allUiViews.Where(v => v.name == "UIView").FirstOrDefault();
            return mainUiView;
        }
    }
}
