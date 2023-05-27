using System;
using System.Collections.Generic;
using GenesisConstructa.SetupSystem.StackZones.ComponentsBuilding;
using GenesisConstructa.Utility;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.StackZones
{
    [Serializable]
    public class StackZoneSetupBuildController
    {
        [SerializeField] private SetupScheme zoneSchemePrefab;
        
        [SerializeField, HideInInspector] private StackZoneSetupData data;
        private GameObject _rootGameObject;

        private StackZoneStructureBuilder _structureBuilder;
        private StackZoneComponentsBuildController _componentsBuildController;


        public void Build(GameObject rootGameObject, StackZoneSetupData data)
        {
            _rootGameObject = rootGameObject;
            
            if (data == null)
            {
                (_structureBuilder ??= new StackZoneStructureBuilder(_rootGameObject, zoneSchemePrefab)).Clear();
                (_componentsBuildController ??= new StackZoneComponentsBuildController(_rootGameObject, zoneSchemePrefab)).Clear();
                return;
            }
            
            if (Equals(this.data, data))
            {
                return;
            }

            List<string> changedProperties = DeepComparer.Compare(this.data, data);

            this.data = data;

            (_structureBuilder ??= new StackZoneStructureBuilder(_rootGameObject, zoneSchemePrefab)).Build(this.data);
            (_componentsBuildController ??= new StackZoneComponentsBuildController(_rootGameObject, zoneSchemePrefab)).Build(this.data, changedProperties);
        }
    }
}