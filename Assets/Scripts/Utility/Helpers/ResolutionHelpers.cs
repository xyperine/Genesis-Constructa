using UnityEngine;

namespace ColonizationMobileGame.Utility.Helpers
{
    public static class ResolutionHelpers
    {
        public static Vector2 GetWindowSize()
        {
            return new Vector2(Screen.width, Screen.height);
        }


        public static Vector2 GetScreenResolution()
        {
            return new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        }


        public static Vector2 GetWindowCenter()
        {
            return GetWindowSize() * 0.5f;
        }


        public static Vector2 GetScreenCenter()
        {
            return GetScreenResolution() * 0.5f;
        }
    }
}