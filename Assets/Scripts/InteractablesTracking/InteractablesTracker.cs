using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.Utility.Extensions;
using UnityEngine;

namespace GenesisConstructa.InteractablesTracking
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


        public MonoBehaviour[] GetObjectsInRadiusAround(Vector3 center, float radius, bool ignoreY)
        {
            MonoBehaviour[] objects =
                _idsToObjectsMap.Values.Where(mb => IsObjectInRadius(mb, center, radius, ignoreY)).ToArray();
            return objects;
        }


        private bool IsObjectInRadius(MonoBehaviour obj, Vector3 center, float radius, bool ignoreY)
        {
            if (!obj || !obj.isActiveAndEnabled)
            {
                return false;
            }
            
            Vector3 objPosition = obj.transform.position;
            
            if (ignoreY)
            {
                center = center.XZPlane();
                objPosition = objPosition.XZPlane();
            }
            
            return Vector3.Distance(center, objPosition) <= radius;
        }
    }
}