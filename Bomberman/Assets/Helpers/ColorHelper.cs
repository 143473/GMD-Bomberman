using UnityEngine;

namespace Helpers
{
    public class ColorHelper
    {
        public static Color NormalizeColor(float r, float g, float b, float a)
        {
            return new Color(Random.Range(r, r + 3)/ 255, Random.Range(g, g + 3)/ 255, Random.Range(b, b + 3) / 255, a / 255);
        }
    }
}