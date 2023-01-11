using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.Utility.Extensions
{
    public static class TextureExtensions
    {
        public static Vector2 NonTransparentContentCenter(this Texture2D texture)
        {
            Vector2[] pixelsCoords = GetNonTransparentContentPixelsCoords(texture);
            Vector2 center = GetNonTransparentContentCenterCoord(pixelsCoords);
            
            return center;
        }


        // private static float GetMidPoint(Vector2[] pixelsCoords)
        // {
        //     float min = pixelsCoords.Min(c => c.x);
        //     float max = pixelsCoords.Max(c => c.x);
        //     float mid = min + max;
        //     mid /= 2f;
        //
        //     return mid;
        // }


        private static Vector2[] GetNonTransparentContentPixelsCoords(Texture2D texture)
        {
            Color[] pixels = texture.GetPixels();
            List<Vector2> pixelsCoords = new List<Vector2>();

            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    Color pixelColor = pixels[y * texture.width + x];
                    
                    if (pixelColor.a > 0)
                    {
                        pixelsCoords.Add(new Vector2(x + 1, y + 1));
                    }
                }
            }

            return pixelsCoords.ToArray();
        }


        private static Vector2 GetNonTransparentContentCenterCoord(Vector2[] pixelsCoords)
        {
            float minX = pixelsCoords.Min(c => c.x);
            float maxX = pixelsCoords.Max(c => c.x);
            float midX = minX + maxX;
            midX /= 2f;
            
            float minY = pixelsCoords.Min(c => c.y);
            float maxY = pixelsCoords.Max(c => c.y);
            float midY = minY + maxY;
            midY /= 2f;

            return new Vector2(midX, midY);
        }


        public static float NonTransparentContentBiggestDimensionValue(this Texture2D texture)
        {
            Vector2[] pixelsCoords = GetNonTransparentContentPixelsCoords(texture);
            
            float minX = pixelsCoords.Min(c => c.x);
            float maxX = pixelsCoords.Max(c => c.x);
            float xLength = maxX - minX;

            float minY = pixelsCoords.Min(c => c.y);
            float maxY = pixelsCoords.Max(c => c.y);
            float yLength = maxY - minY;

            float maxLength = Mathf.Max(xLength, yLength);
            
            return maxLength;
        }
    }
}