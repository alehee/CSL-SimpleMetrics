using CSL_SimpleMetrics.Behaviours;
using ICities;
using System.Collections.Generic;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Extensions
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private GameObject _managerGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

             _managerGameObject = new GameObject(Manager.Name);
             _managerGameObject.AddComponent<Manager>();

            Logger.Log("Loaded moddification.");
        }

        public override void OnLevelUnloading()
        {
            var objectsToDestroy = new List<GameObject> {
                _managerGameObject
            };

            for (int i = 0; i < objectsToDestroy.Count; i++)
            {
                if (objectsToDestroy[i] != null)
                {
                    UnityEngine.Object.Destroy(objectsToDestroy[i].gameObject);
                }
            }

            Logger.Log("Unloaded moddification.");
        }
    }
}
