using UnityEngine;

namespace ColonizationMobileGame.Utility.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 XZPlaneVector2(this Vector3 a)
        {
            return new Vector2(a.x, a.z);
        }
        
        
        public static Vector3 XZPlane(this Vector3 a)
        {
            return new Vector3(a.x, 0f, a.z);
        }


        public static Vector3 XZPlaneToVector3(this Vector2 a, float y = 0f)
        {
            return new Vector3(a.x, y, a.y);
        }


        public static Vector3 Abs(this Vector3 a)
        {
            return new Vector3(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z));
        }
    }
}