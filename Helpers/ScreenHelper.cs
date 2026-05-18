using UnityEngine;
using Resolution = CSL_SimpleMetrics.Models.Helpers.Resolution;

namespace CSL_SimpleMetrics.Helpers
{
    public static class ScreenHelper
    {
        public static Vector3 GetDefaultWindowPosition()
        {
            return new Vector3(180,10);
        }

        public static Resolution GetResolution()
        {
            return new Resolution
            {
                Width = Screen.width,
                Height = Screen.height
            };
        }
    }
}
