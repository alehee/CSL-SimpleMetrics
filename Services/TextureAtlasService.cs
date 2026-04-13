using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Logging;
using CSL_SimpleMetrics.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Logger = CSL_SimpleMetrics.Logging.Logger;

namespace CSL_SimpleMetrics.Services
{
    public class TextureAtlasService
    {
        Texture2D _baseTexture => new Texture2D(1, 1, TextureFormat.ARGB32, false);

        public UITextureAtlas GetAtlas()
        {
            var spriteNamesAndPaths = GetSpriteNamesAndPaths();
            var spriteCount = spriteNamesAndPaths.Count;

            var textures = new Texture2D[spriteCount];
            var regions = new Rect[spriteCount];

            int textureCounter = 0;
            foreach (var spriteNameAndPath in spriteNamesAndPaths)
            {
                textures[textureCounter] = LoadTextureFromAssembly(spriteNameAndPath.Value);
                textureCounter++;
            }

            regions = _baseTexture.PackTextures(textures, 2, 512);

            Material material = Object.Instantiate(UIView.GetAView().defaultAtlas.material);
            material.mainTexture = _baseTexture;

            UITextureAtlas atlas = ScriptableObject.CreateInstance<UITextureAtlas>();
            atlas.material = material;
            atlas.name = $"{AppInformation.AppPrefix}_Atlas";

            var spriteCounter = 0;
            foreach (var spriteNameAndPath in spriteNamesAndPaths)
            {
                UITextureAtlas.SpriteInfo sprite = new UITextureAtlas.SpriteInfo
                {
                    name = spriteNameAndPath.Key,
                    texture = textures[spriteCounter],
                    region = regions[spriteCounter]
                };
                atlas.AddSprite(sprite);
                spriteCounter++;
            }

            return atlas;
        }

        private Texture2D LoadTextureFromAssembly(string path)
        {
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

            byte[] array = new byte[resourceStream.Length];
            resourceStream.Read(array, 0, array.Length);

            Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.LoadImage(array);

            return texture;
        }

        private Dictionary<string, string> GetSpriteNamesAndPaths()
        {
            var iconNames = System.Enum.GetValues(typeof(MetricsEnum))
                .Cast<MetricsEnum>()
                .Select(metric => metric.ToString())
                .ToArray();
            var backgroundNames = new string[] { "Background" };

            var result = new Dictionary<string, string>();

            foreach (var iconName in iconNames)
            {
                result[iconName] = $"Assets.Icons.{iconName}.png";
                Logger.Log($"Added sprite with name {iconName} and path {result[iconName]}");
            }

            foreach (var backgroundName in backgroundNames)
            {
                result[backgroundName] = $"Assets.Backgrounds.{backgroundName}.png";
                Logger.Log($"Added sprite with name {backgroundName} and path {result[backgroundName]}");
            }

            return result;
        }
    }
}
