using ColossalFramework.UI;
using CSL_SimpleMetrics.Models;
using System.Linq;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.UI
{
    public class Window : UIPanel
    {
        public static string Name => "SimpleMetricsWindow";

        private UIView _parent;

        private WindowSettings _windowSettings;

        public override void Start()
        {
            var allUiViews = GameObject.FindObjectsOfType<UIView>();
            var mainUiView = allUiViews.Where(v => v.name == "UIView").FirstOrDefault();
            if (mainUiView == null)
            {
                Logger.Log("Unable to find main UIView", Logging.LogLevelEnum.Error);
                return;
            }
            
            _parent = mainUiView;
            this.transform.parent = _parent.transform;
            this.relativePosition = new Vector3(100, 100); // TODO here's just a placeholder

            _windowSettings = new WindowSettings();

            base.Start();

            ApplyWindowSettings();
        }

        private void ApplyWindowSettings()
        {
            // TODO adjust it later
            this.backgroundSprite = "MenuPanel";
            this.isVisible = true;
            this.canFocus = true;
            this.isInteractive = true;
            this.width = _windowSettings.Width;
            this.height = _windowSettings.Height;
        }
    }
}
