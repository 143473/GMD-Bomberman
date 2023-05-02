using UnityEngine;
using UnityEngine.UIElements;

namespace Helpers
{
    public static class StageHelper
    {
        public static Color BrickGradient()
        {
            return RandomColor(150f, 75f, 0f, 1f, 10f);
        }
        public static Color StoneGradient()
        {
            return RandomColor(128f, 128f, 128f, 1f, 5f);
            
        }
        public static float BrickWallHeight()
        {
            return Random.Range(1.0f, 1.5f);
        } 
        public static float StoneWallHeight()
        {
            return Random.Range(1.1f, 1.6f);
        }
        private static Color RandomColor(float g,float r,float b, float a, float offset)
        {
            return new Color(Random.Range(g - offset, g + offset)/ 255, 
                Random.Range(r - offset, r + offset)/ 255, 
                Random.Range(b-offset, b + offset) / 255, a / 255);
        }
    }
     
}