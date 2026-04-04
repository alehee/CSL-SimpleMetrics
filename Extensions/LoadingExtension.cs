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
        private GameObject _mainGameObject;
        private GameObject _managerGameObject;
        private GameObject _windowGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            _mainGameObject = new GameObject(AppInformation.AppPrefix);
            _mainGameObject.transform.parent = GetUIView().transform;
            _mainGameObject.AddComponent<Manager>();
            _windowGameObject = new GameObject(Window.Name);
            _windowGameObject.transform.parent = _mainGameObject.transform;
            _windowGameObject.AddComponent<Window>();

            //CreateWindow();

            Logger.Log("Loaded moddification.");
        }

        public override void OnLevelUnloading()
        {
            var objectsToDestroy = new List<GameObject> {
                // If you are adding more game objects, add them here to be destroyed on unload.
                _mainGameObject
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

        private UIView GetUIView()
        {
            var allUiViews = GameObject.FindObjectsOfType<UIView>();
            var mainUiView = allUiViews.Where(v => v.name == "UIView").FirstOrDefault();
            return mainUiView;
        }

        private void CreateWindow()
        {
            _windowGameObject = new GameObject(Window.Name);
            _windowGameObject.transform.parent = _managerGameObject.transform;
            _windowGameObject.AddComponent<Window>();
        }
    }
}
