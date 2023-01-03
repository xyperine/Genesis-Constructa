using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class InteractablesTracker : MonoBehaviour
    {
        private readonly Dictionary<int, MonoBehaviour> _idsToObjectsMap = new Dictionary<int, MonoBehaviour>();

        public IReadOnlyDictionary<int, MonoBehaviour> IDsToObjectsMap => _idsToObjectsMap;


        public void RegisterObject(MonoBehaviour obj)
        {
            if (!obj)
            {
                return;
            }

            _idsToObjectsMap.TryAdd(obj.GetInstanceID(), obj);
        }


        public int[] GetObjectIDsInRadiusAround(Vector3 center, float radius)
        {
            int[] ids = _idsToObjectsMap.Where(kvp => IsObjectInRadius(kvp.Value, center, radius))
                .Select(kvp => kvp.Key).ToArray();
            return ids;
        }


        private bool IsObjectInRadius(MonoBehaviour obj, Vector3 center, float radius)
        {
            return obj &&
                   obj.isActiveAndEnabled &&
                   Vector3.Distance(center, obj.transform.position) <= radius;
        }
    }
}