using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem
{
    public class SetupScheme : MonoBehaviour
    {
        private readonly Dictionary<SetupMarker, IEnumerable<Component>> _allowedComponentsMap =
            new Dictionary<SetupMarker, IEnumerable<Component>>();

        private void OnValidate()
        {
            Remap();
        }


        private void Remap()
        {
            _allowedComponentsMap.Clear();

            IEnumerable<SetupMarker> children = GetComponentsInChildren<SetupMarker>();
            foreach (SetupMarker child in children)
            {
                MapAllowedComponentsFor(child);
            }
        }


        private void MapAllowedComponentsFor(SetupMarker marker)
        {
            _allowedComponentsMap.TryAdd(marker, marker.GetComponents<Component>().Except(new[] {marker.transform}));
        }


        public Component[] GetComponentsOf(Type markerType)
        {
            SetupMarker keyMarker = _allowedComponentsMap.Keys.FirstOrDefault(m => m.GetType() == markerType);

            if (!keyMarker)
            {
                Debug.LogError("Unable to find game object with this name");
                return null;
            }

            return _allowedComponentsMap[keyMarker].ToArray();
        }
    }
}