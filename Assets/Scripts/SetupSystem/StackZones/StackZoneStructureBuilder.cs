using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.SetupSystem.StackZones.Markers;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    public class StackZoneStructureBuilder
    {
        private readonly GameObject _rootGameObject;
        private readonly SetupScheme _zoneSchemePrefab;
        private StackZoneSetupData _data;


        public StackZoneStructureBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab)
        {
            _rootGameObject = rootGameObject;
            _zoneSchemePrefab = zoneSchemePrefab;
        }


        public void Build(StackZoneSetupData data)
        {
            _data = data;
            
            BuildStructure();
        }


        private void BuildStructure()
        {
            AddMissingObjects();
            RemoveAuxiliaryObjects();
        }


        private void AddMissingObjects()
        {
            IEnumerable<string> children = _rootGameObject.transform.Cast<Transform>().Select(t => t.name)
                .Except(new[] {_rootGameObject.name});
            
            IEnumerable<string> childrenFromScheme = _zoneSchemePrefab.transform.Cast<Transform>().Select(t => t.name)
                .Except(new[] {_zoneSchemePrefab.name});
            
            IEnumerable<string> missingChildren = childrenFromScheme.Except(children);
            
            foreach (string missingChild in missingChildren)
            {
                Transform childFromScheme = _zoneSchemePrefab.transform.Find(missingChild);
                Transform child = Object.Instantiate(childFromScheme, _rootGameObject.transform);
                child.name = child.name.Replace("(Clone)", "");
            }
        }


        private void RemoveAuxiliaryObjects()
        {
            if (!_data.InteractWithOthers)
            {
                Object.DestroyImmediate(_rootGameObject.GetGameObjectByMarker(typeof(StackZoneBehavioursSetupMarker)));
            }

            if (!_data.InteractWithPlayer)
            {
                Object.DestroyImmediate(_rootGameObject.GetGameObjectByMarker(typeof(InteractionWithPlayerSetupMarker)));
            }

            if (!_data.UpgradeableOnItsOwn)
            {
                Object.DestroyImmediate(_rootGameObject.GetGameObjectByMarker(typeof(UpgraderSetupSetupMarker)));
            }
        }


        public void Clear()
        {
            GameObject[] s = _rootGameObject.transform.Cast<Transform>().Select(t => t.gameObject)
                .Except(new[] {_rootGameObject}).ToArray();
            
            foreach (GameObject child in s)
            {
                Object.DestroyImmediate(child);
            }

            _data = null;
        }
    }
}