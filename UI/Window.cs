using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Factories;
using CSL_SimpleMetrics.Models;
using CSL_SimpleMetrics.Services;
using System.Collections.Generic;
using UnityEngine;

namespace CSL_SimpleMetrics.UI
{
    public class Window : UIPanel
    {
        public static string Name => $"{AppInformation.AppPrefix}_Window";

        private GameObject _windowGameObject;

        private UIPanel _bodyPanel;
        private UITextureAtlas _atlas;
        private Dictionary<MetricsEnum, UILabel> _labels;

        private WindowSettings _windowSettings;

        private UIFactory _uiFactory;
        private MetricsService _metricsService;
        private TextureAtlasService _textureAtlasService;

        public override void Start()
        {
            this.relativePosition = new Vector3(100, 100); // TODO here's just a placeholder

            _windowSettings = new WindowSettings(); // TODO load settings from file       

            base.Start();

            _textureAtlasService = new TextureAtlasService();
            _atlas = _textureAtlasService.GetAtlas();

            _windowGameObject = CreateWindowGameObject();
            _bodyPanel = CreateBodyPanel();
            CreateDragHandler();

            _labels = new Dictionary<MetricsEnum, UILabel>();

            _uiFactory = new UIFactory(_windowGameObject.transform);

            _metricsService = MetricsService.GetInstance();
            _metricsService.MetricsUpdated += OnMetricsUpdated;

            // TODO remove it later
            CreateTestLabels();
        }

        private void OnMetricsUpdated()
        {
            MetricsCombined metrics = _metricsService.GetMetrics();
            // TODO change temp metrics to proper complete data
            if (
                _labels.ContainsKey(MetricsEnum.Electricity) && 
                _labels.ContainsKey(MetricsEnum.Water) &&
                _labels.ContainsKey(MetricsEnum.Sewage)
            )
            {
                _labels[MetricsEnum.Electricity].text = $"{MetricsEnum.Electricity.ToString()}: {metrics.Get(MetricsEnum.Electricity).ToString()}";
                _labels[MetricsEnum.Water].text = $"{MetricsEnum.Water.ToString()}: {metrics.Get(MetricsEnum.Water).ToString()}";
                _labels[MetricsEnum.Sewage].text = $"{MetricsEnum.Sewage.ToString()}: {metrics.Get(MetricsEnum.Sewage).ToString()}";
            }

        }

        // TODO remove it later, testing method
        private void CreateTestLabels()
        {
            _labels[MetricsEnum.Electricity] = _uiFactory
                .CreateLabel(MetricsEnum.Electricity.ToString(), "Hello, World");
            _labels[MetricsEnum.Water] = _uiFactory.
                CreateLabel(MetricsEnum.Water.ToString(), "Hello, __World", -0.05f);
            _labels[MetricsEnum.Sewage] = _uiFactory
                .CreateLabel(MetricsEnum.Sewage.ToString(), "Hello, ____World", -0.1f);
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
            panel.atlas = _atlas;
            panel.backgroundSprite = "Background";
            panel.isVisible = true;
            panel.canFocus = true;
            panel.opacity = 0.8f;
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
