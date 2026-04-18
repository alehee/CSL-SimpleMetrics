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
        private Dictionary<MetricsEnum, UILabel> _labels; // Temporary objects
        private Dictionary<MetricsEnum, UISprite> _sprites;
        private Dictionary<MetricsEnum, UISprite> _indicatorSprites;

        private WindowSettings _windowSettings;

        private UIFactory _uiFactory;
        private MetricsService _metricsService;
        private TextureAtlasService _textureAtlasService;

        public override void Start()
        {
            this.relativePosition = new Vector3(50, 50); // TODO here's just a placeholder

            _windowSettings = new WindowSettings(); // TODO maybe in the future -> load from config file

            base.Start();

            _textureAtlasService = new TextureAtlasService();
            _atlas = _textureAtlasService.GetAtlas();

            _windowGameObject = CreateWindowGameObject();
            _bodyPanel = CreateBodyPanel();
            CreateDragHandler();

            _labels = new Dictionary<MetricsEnum, UILabel>();
            _sprites = new Dictionary<MetricsEnum, UISprite>();
            _indicatorSprites = new Dictionary<MetricsEnum, UISprite>();

            _uiFactory = new UIFactory(_windowGameObject.transform, _atlas);

            _metricsService = MetricsService.GetInstance();
            _metricsService.MetricsUpdated += OnMetricsUpdated;

            // TODO remove it later
            //CreateTestLabels();

            CreateMetricsSprites();
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

        // TODO add other metrics sprites
        private void CreateMetricsSprites()
        {
            float horizontalMargin = 0.01f;
            foreach (MetricsEnum metric in _metricsService.GetMetrics().Keys)
            {
                _indicatorSprites[metric] = _uiFactory.CreateSprite(
                    name: "Indicator",
                    horizontalMargin: horizontalMargin,
                    size: 0.7f,
                    zOrderEnum: WindowZOrderEnum.Indicator,
                    opacity: 0.5f,
                    color: Color.red
                );

                _sprites[metric] = _uiFactory.CreateSprite(
                    name: metric.ToString(), 
                    horizontalMargin: horizontalMargin,
                    size: 0.7f
                );

                horizontalMargin += 0.05f;
            }
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
            panel.isInteractive = true;
            panel.width = _windowSettings.Width;
            panel.height = _windowSettings.Height;
            panel.padding = new RectOffset(10, 10, 10, 10);
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
