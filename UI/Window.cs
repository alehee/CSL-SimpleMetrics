using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Factories;
using CSL_SimpleMetrics.Models;
using System.Collections.Generic;
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

            _windowSettings = new WindowSettings(); // TODO load settings from file       

            base.Start();

            _windowGameObject = CreateWindowGameObject();
            _bodyPanel = CreateBodyPanel();
            CreateDragHandler();

            _uiFactory = new UIFactory(_windowGameObject.transform);

            // TODO remove it later
            CreateTestLabels();
        }

        // TODO remove it later, testing method
        private void CreateTestLabels()
        {
            _uiFactory.CreateLabel("TestLabel", "Hello, World");
            _uiFactory.CreateLabel("TestLabel2", "Hello, __World", -0.05f);
            _uiFactory.CreateLabel("TestLabel3", "Hello, ____World", -0.1f);
        }

        private GameObject CreateWindowGameObject()
        {
            var gameObject = new GameObject(Name);
            gameObject.transform.parent = GameObject.Find(AppInformation.AppPrefix).transform;
            this.width = _windowSettings.Width;
            this.height = _windowSettings.Height;

            return gameObject;
        }

        private UIPanel CreateBodyPanel()
        {
            var panel = _windowGameObject.AddComponent<UIPanel>();
            panel.backgroundSprite = "MenuPanel";
            panel.isVisible = true;
            panel.canFocus = true;
            panel.isInteractive = true;
            panel.width = _windowSettings.Width;
            panel.height = _windowSettings.Height;
            panel.relativePosition = Vector3.zero;
            panel.zOrder = (int)WindowZOrderEnum.Background;

            return panel;
        }

        private void CreateDragHandler()
        {
            var dragHandlerGameObject = new GameObject("DragHandler");
            dragHandlerGameObject.transform.parent = this.transform;
            dragHandlerGameObject.transform.localPosition = Vector3.zero;
            var dragHandler = dragHandlerGameObject.AddComponent<UIDragHandle>();
            dragHandler.width = _bodyPanel.width;
            dragHandler.height = _bodyPanel.height;
            dragHandler.zOrder = (int)WindowZOrderEnum.Foreground;
            dragHandler.BringToFront();
        }
    }
}
