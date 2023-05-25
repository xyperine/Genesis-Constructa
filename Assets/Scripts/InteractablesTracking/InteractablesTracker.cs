using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.InteractablesTracking
{
    public class InteractablesTracker : MonoBehaviour
    {
        private readonly Dictionary<int, MonoBehaviour> _idsToObjectsMap = new Dictionary<int, MonoBehaviour>();


        public void RegisterObject(MonoBehaviour obj)
        {
            if (!obj)
            {
                return;
            }

            _idsToObjectsMap.TryAdd(obj.GetInstanceID(), obj);
        }


        public MonoBehaviour[] GetObjectsInRadiusAround(Vector3 center, float radius)
        {
            MonoBehaviour[] objects =
                _idsToObjectsMap.Values.Where(mb => IsObjectInRadius(mb, center, radius)).ToArray();
            return objects;
        }


        private bool IsObjectInRadius(MonoBehaviour obj, Vector3 center, float radius)
        {
            return obj &&
                   obj.isActiveAndEnabled &&
                   Vector3.Distance(center, obj.transform.position) <= radius;
        }
    }
}