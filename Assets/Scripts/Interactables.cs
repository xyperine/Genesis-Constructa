using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class Interactables
    {
        public static Dictionary<int, MonoBehaviour> IDsToObjectsMap { get; } = new Dictionary<int, MonoBehaviour>();
        
        
        public static void RegisterObject(MonoBehaviour obj)
        {
            if (!obj)
            {
                return;
            }
            
            IDsToObjectsMap.TryAdd(obj.GetInstanceID(), obj);
        }


        public static int[] GetObjectIDsInRadiusAround(Vector3 center, float radius)
        {
            int[] ids = IDsToObjectsMap.Where(kvp => IsObjectInRadius(kvp.Value, center, radius))
                .Select(kvp => kvp.Key).ToArray();
            return ids;
        }


        private static bool IsObjectInRadius(MonoBehaviour obj, Vector3 center, float radius)
        {
            return obj &&
                   obj.isActiveAndEnabled &&
                   Vector3.Distance(center, obj.transform.position) <= radius;
        }
    }
}