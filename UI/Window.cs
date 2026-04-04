using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Factories;
using CSL_SimpleMetrics.Models;
using System.Linq;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.UI
{
    public class Window : UIPanel
    {
        public static string Name => $"{AppInformation.AppPrefix}_Window";

        private UIView _parent;

        private WindowSettings _windowSettings;

        private UiFactory _uiFactory;

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
            CreateDragHandler();

            _uiFactory = new UiFactory(this.transform);
            // TODO remove it later
            CreateTestLabels();
        }

        // TODO remove it later, testing method
        private void CreateTestLabels()
        {
            var label = _uiFactory.CreateLabel("TestLabel");
            label.text = "Hello, World!";
            //label.cachedTransform.parent = this.transform;
            label.zOrder = 1; // TODO ensure what's going on here
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

        private void CreateDragHandler()
        {
            var dragHandlerGameObject = new GameObject("DragHandler");
            dragHandlerGameObject.transform.parent = this.transform;
            dragHandlerGameObject.transform.localPosition = Vector3.zero;
            var dragHandler = dragHandlerGameObject.AddComponent<UIDragHandle>();
            dragHandler.width = this.width;
            dragHandler.height = this.height;
            dragHandler.zOrder = 0;
            dragHandler.BringToFront();
        }
    }
}
