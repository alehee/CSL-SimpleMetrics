using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Factories;
using CSL_SimpleMetrics.Helpers;
using CSL_SimpleMetrics.Models;
using CSL_SimpleMetrics.Models.Helpers;
using CSL_SimpleMetrics.Services;
using System.Collections.Generic;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.UI
{
    public class Window : UIPanel
    {
        public static string Name => $"{AppInformation.AppPrefix}_Window";

        private GameObject _windowGameObject;

        private UIPanel _bodyPanel;

        private GameObject _dragHandlerGameObject;

        private UITextureAtlas _atlas;
        private Dictionary<MetricsEnum, SpriteAndLocale> _sprites;
        private Dictionary<MetricsEnum, UISprite> _indicatorSprites;

        private WindowSettings _windowSettings;

        private UIFactory _uiFactory;
        private MetricsService _metricsService;
        private TextureAtlasService _textureAtlasService;

        private bool _isMouseOverPanel = false;
        private bool _isMouseOverDragHandler = false;

        private bool _isDebug = false; // Helper variable to enable some debug logs

        public override void Start()
        {
            this.relativePosition = new Vector3(50, 50); // TODO here's just a placeholder

            _windowSettings = new WindowSettings(); // TODO maybe in the future -> load from config file

            base.Start();

            _textureAtlasService = new TextureAtlasService();
            _atlas = _textureAtlasService.GetAtlas();

            _windowGameObject = CreateWindowGameObject();
            _bodyPanel = CreateBodyPanel();

            _dragHandlerGameObject = CreateDragHandler();

            CreateMouseEvents();

            _sprites = new Dictionary<MetricsEnum, SpriteAndLocale>();
            _indicatorSprites = new Dictionary<MetricsEnum, UISprite>();

            _uiFactory = new UIFactory(_windowGameObject.transform, _atlas);

            _metricsService = MetricsService.GetInstance();
            _metricsService.MetricsUpdated += OnMetricsUpdated;

            CreateMetricsSprites();
        }

        private void OnMetricsUpdated()
        {
            OnMetricsUpdated(_indicatorSprites);
        }

        private void OnMetricsUpdated(Dictionary<MetricsEnum, UISprite> _indicatorSprites)
        {
            MetricsCombined metrics = _metricsService.GetMetrics();

            if (_sprites.Count < metrics.Keys.Count) return;

            foreach (MetricsEnum metric in metrics.Keys)
            {
                var indicatorSprite = _indicatorSprites[metric];
                SpriteHelper.ChangeSpriteColor(ref indicatorSprite, metrics.Get(metric).Ratio, _windowSettings.IndicatorOpacity);

                _sprites[metric].Sprite.tooltip = SpriteHelper.GetFormattedTooltip(
                    _sprites[metric].Locale, 
                    metrics.Get(metric).Ratio
                );

                if (_isDebug)
                {
                    Logger.Log($"{metric.ToString()}: {metrics.Get(metric).Ratio}");
                    Logger.Log($"{metrics.Get(metric).ToString()}");
                }
            }
        }

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
                    opacity: _windowSettings.IndicatorOpacity,
                    color: new Color(0f, 0f, 0f)
                );

                var iconSprite = _uiFactory.CreateSprite(
                    name: metric.ToString(),
                    horizontalMargin: horizontalMargin,
                    size: 0.7f
                );
                string spriteLocaleString = LocaleHelper.GetMetricsLocaleString(metric);
                iconSprite.tooltip = SpriteHelper.GetFormattedTooltip(spriteLocaleString, 0f);
                _sprites[metric] = new SpriteAndLocale { Sprite = iconSprite, Locale = spriteLocaleString };

                horizontalMargin += 0.045f;
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
            panel.padding = new RectOffset(5, 5, 5, 5);
            panel.relativePosition = Vector3.zero;
            panel.zOrder = (int)WindowZOrderEnum.Background;

            return panel;
        }

        private GameObject CreateDragHandler()
        {
            var dragHandlerGameObject = new GameObject("DragHandler");
            dragHandlerGameObject.transform.parent = this.transform;
            dragHandlerGameObject.transform.localPosition = Vector3.zero;

            var dragHandler = dragHandlerGameObject.AddComponent<UIDragHandle>();
            dragHandler.width = _windowSettings.Height / 4;
            dragHandler.height = _windowSettings.Height / 4;
            dragHandler.zOrder = (int)WindowZOrderEnum.Content;
            dragHandler.isInteractive = true;
            dragHandler.target = this;

            UIFactory dragHandlerUiFactory = new UIFactory(dragHandlerGameObject.transform, _atlas);
            var dragSprite = dragHandlerUiFactory.CreateSprite(
                name: "Move",
                zOrderEnum: WindowZOrderEnum.Background
            );
            dragSprite.width = dragHandler.width;
            dragSprite.height = dragHandler.height;
            dragSprite.isVisible = false;

            return dragHandlerGameObject;
        }

        private void CreateMouseEvents()
        {
            _bodyPanel.eventMouseEnter += (component, eventParam) =>
            {
                _isMouseOverPanel = true;
                _dragHandlerGameObject.GetComponentInChildren<UISprite>().isVisible = true;
            };
            _bodyPanel.eventMouseLeave += (component, eventParam) =>
            {
                _isMouseOverPanel = false;
                CheckMouseOver();
            };
            _dragHandlerGameObject.GetComponent<UIDragHandle>().eventMouseEnter += (component, eventParam) =>
            {
                _isMouseOverDragHandler = true;
                _dragHandlerGameObject.GetComponentInChildren<UISprite>().isVisible = true;
            };
            _dragHandlerGameObject.GetComponent<UIDragHandle>().eventMouseLeave += (component, eventParam) =>
            {
                _isMouseOverDragHandler = false;
                CheckMouseOver();
            };
        }

        private void CheckMouseOver()
        {
            if (!_isMouseOverPanel && !_isMouseOverDragHandler)
            {
                _dragHandlerGameObject.GetComponentInChildren<UISprite>().isVisible = false;
            }
        }
    }
}
