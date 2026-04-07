using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Factories;
using CSL_SimpleMetrics.Models;
using System.Linq;
using UnityEngine;

namespace CSL_SimpleMetrics.UI
{
    public class Window : UIPanel
    {
        public static string Name => $"{AppInformation.AppPrefix}_Window";

        private GameObject _windowGameObject;

        private UIPanel _bodyPanel;

        private WindowSettings _windowSettings;

        private UIFactory _uiFactory;

        public override void Start()
        {
            this.relativePosition = new Vector3(100, 100); // TODO here's just a placeholder

            _windowSettings = new WindowSettings();            

            base.Start();

            CreateWindowGameObject();
            CreateBodyPanel();
            CreateDragHandler();

            _uiFactory = new UIFactory(_windowGameObject.transform);

            // TODO remove it later
            CreateTestLabels();
        }

        // TODO remove it later, testing method
        private void CreateTestLabels()
        {
            var label = _uiFactory.CreateLabel("TestLabel");
            label.text = "Hello, World!";
            label.zOrder = 1; // TODO ensure what's going on here
        }

        private void CreateWindowGameObject()
        {
            _windowGameObject = new GameObject(Name);
            _windowGameObject.transform.parent = GameObject.Find(AppInformation.AppPrefix).transform;
            this.width = _windowSettings.Width;
            this.height = _windowSettings.Height;
        }

        private void CreateBodyPanel()
        {
            _bodyPanel = _windowGameObject.AddComponent<UIPanel>();
            _bodyPanel.backgroundSprite = "MenuPanel";
            _bodyPanel.isVisible = true;
            _bodyPanel.canFocus = true;
            _bodyPanel.isInteractive = true;
            _bodyPanel.width = _windowSettings.Width;
            _bodyPanel.height = _windowSettings.Height;
        }

        private void CreateDragHandler()
        {
            var dragHandlerGameObject = new GameObject("DragHandler");
            dragHandlerGameObject.transform.parent = _bodyPanel.transform;
            dragHandlerGameObject.transform.localPosition = Vector3.zero;
            var dragHandler = dragHandlerGameObject.AddComponent<UIDragHandle>();
            dragHandler.width = _bodyPanel.width;
            dragHandler.height = _bodyPanel.height;
            dragHandler.zOrder = 0;
            dragHandler.BringToFront();
        }
    }
}
