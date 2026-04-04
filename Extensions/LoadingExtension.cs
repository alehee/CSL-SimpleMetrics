using CSL_SimpleMetrics.Behaviours;
using CSL_SimpleMetrics.UI;
using ICities;
using System.Collections.Generic;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Extensions
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private GameObject _managerGameObject;
        private GameObject _windowGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            _managerGameObject = new GameObject(Manager.Name);
            _managerGameObject.AddComponent<Manager>();
            _managerGameObject.AddComponent<Window>(); // Test it bruh

            Logger.Log("Loaded moddification.");
        }

        public override void OnLevelUnloading()
        {
            var objectsToDestroy = new List<GameObject> {
                // If you are adding more game objects, add them here to be destroyed on unload.
                _managerGameObject
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
    }
}
