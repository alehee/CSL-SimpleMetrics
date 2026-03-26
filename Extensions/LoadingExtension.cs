using ICities;
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

            Logger.Log("Loaded moddification.");
            // TODO: add initialization code here
        }

        public override void OnLevelUnloading()
        {
            Logger.Log("Unloaded moddification.");
            // TODO: add cleanup code here
        }
    }
}
