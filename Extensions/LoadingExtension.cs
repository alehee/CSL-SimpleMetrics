using CSL_SimpleMetrics.Logging;
using ICities;

namespace CSL_SimpleMetrics.Extensions
{
    public class LoadingExtension : LoadingExtensionBase
    {
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
