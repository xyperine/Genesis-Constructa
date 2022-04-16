using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    public class StackZoneScheme : MonoBehaviour
    {
        [SerializeField] private GameObject placementObject;
        [SerializeField] private GameObject interactorObject;
        [SerializeField] private GameObject colliderObject;
        [SerializeField] private GameObject behavioursObject;
        [SerializeField] private GameObject upgraderObject;

        private readonly Dictionary<GameObject, IEnumerable<Component>> _allowedComponents = new Dictionary<GameObject, IEnumerable<Component>>();

        private void OnValidate()
        {
            _allowedComponents.Clear();

            _allowedComponents.TryAdd(placementObject, placementObject.GetComponents<Component>());
            _allowedComponents.TryAdd(interactorObject, interactorObject.GetComponents<Component>());
            _allowedComponents.TryAdd(colliderObject, colliderObject.GetComponents<Component>());
            _allowedComponents.TryAdd(behavioursObject, behavioursObject.GetComponents<Component>());
            _allowedComponents.TryAdd(upgraderObject, upgraderObject.GetComponents<Component>());
        }


        public string GetNameOfGameObjectFor(Type componentType)
        {
            GameObject obj = _allowedComponents.FirstOrDefault(kvp => kvp.Value
                    .Select(v => v.GetType()).Any(t => t == componentType || t.IsSubclassOf(componentType)))
                .Key;

            return Helpers.GetGameObjectPathWithoutRoot(obj.transform);
        }
    }
}