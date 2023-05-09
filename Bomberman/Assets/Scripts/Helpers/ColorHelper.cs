using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helpers
{
    public static class ColorHelper
    {
        private static List<Color> brickColors = new()
        {
            NormalizedColor(139, 69, 19), 
            NormalizedColor(160, 82, 45)
        };
        
        public static Color BrickGradient()
        {
            return brickColors[Random.Range(0, 2)];
        }
        
        public static Color StoneGradient()
        {
            return NormalizedColor(128f, 128f, 128f);
        }
        
        public static Color GetPlayerColor(int aiPlayer)
        {
            var colors = new List<Color>
            {
                NormalizedColor(0, 0, 255),
                NormalizedColor(144, 238, 144),
                NormalizedColor(238, 130, 238),
                NormalizedColor(255, 127, 50)
            };
            return colors[aiPlayer];
        }

        private static Color NormalizedColor(float r, float g, float b)
        {
            return new Color(r / 255, g / 255, b / 255, 1);
        }
        
        public static float BrickWallHeight()
        {
            return Random.Range(1.0f, 1.5f);
        } 
        
        public static float StoneWallHeight()
        {
            return Random.Range(1.1f, 1.6f);
        }
    }
}