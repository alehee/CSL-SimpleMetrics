using ColossalFramework.UI;
using CSL_SimpleMetrics.Configuration;
using CSL_SimpleMetrics.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CSL_SimpleMetrics.Services
{
    public class TextureAtlasService
    {
        Texture2D _baseTexture;

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

            _baseTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
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

                // TODO adjust it to the sprite
                if (sprite.name == "Background")
                {
                    int left = 8;
                    int right = 8;
                    int top = 8;
                    int bottom = 8;

                    // If your background texture is larger or corners thicker, increase values accordingly.
                    sprite.border = new RectOffset(left, right, top, bottom);
                }

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
                result[iconName] = $"{AppInformation.AppPrefix}.Assets.Icons.{iconName}.png";
            }

            foreach (var backgroundName in backgroundNames)
            {
                result[backgroundName] = $"{AppInformation.AppPrefix}.Assets.Backgrounds.{backgroundName}.png";
            }

            return result;
        }
    }
}
