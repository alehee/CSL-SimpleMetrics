using ColossalFramework.UI;
using UnityEngine;
using Resolution = CSL_SimpleMetrics.Models.Helpers.Resolution;

namespace CSL_SimpleMetrics.Helpers
{
    public static class SpriteHelper
    {
        public static void ChangeSpriteColor(ref UISprite sprite, float ratio, float opacity)
        {
            sprite.color = GetGradientColor(ratio);
            sprite.opacity = opacity;
        }

        public static string GetFormattedTooltip(string locale, float ratio)
        {
            int percentage = Mathf.Clamp(Mathf.RoundToInt(ratio * 100), 0, 100);

            return $"{locale}\n{percentage}%";
        }

        public static float GetHorizontalMultiplier(Resolution resolution) => 1920f / resolution.Width;

        private static Color GetGradientColor(float ratio)
        {
            float r = 1f;
            float g = 1f;

            if (ratio < 0.5f)
            {
                float t = ratio / 0.5f;

                r = 1f;
                g = Mathf.Lerp(0f, 1f, t);
            }
            else
            {
                float t = (ratio - 0.5f) / 0.5f;
                r = Mathf.Lerp(1f, 0f, t);
                g = 1f;
            }

            return new Color(r, g, 0f);
        }
    }
}
