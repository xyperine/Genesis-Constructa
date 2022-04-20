using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem
{
    public class SetupScheme : MonoBehaviour
    {
        private readonly Dictionary<GameObject, IEnumerable<Component>> _allowedComponentsMap =
            new Dictionary<GameObject, IEnumerable<Component>>();

        private void OnValidate()
        {
            Remap();
        }


        private void Remap()
        {
            _allowedComponentsMap.Clear();

            IEnumerable<Transform> children = GetComponentsInChildren<Transform>().Except(new []{transform});
            foreach (Transform child in children)
            {
                MapAllowedComponentsFor(child.gameObject);
            }
        }


        private void MapAllowedComponentsFor(GameObject obj)
        {
            _allowedComponentsMap.TryAdd(obj, obj.GetComponents<Component>());
        }


        public string GetNameOfGameObjectFor(Type componentType)
        {
            GameObject obj = _allowedComponentsMap.FirstOrDefault(kvp => kvp.Value
                    .Select(v => v.GetType()).Any(t => t == componentType || t.IsSubclassOf(componentType)))
                .Key;

            return Helpers.GetGameObjectPathWithoutRoot(obj.transform);
        }
    }
}