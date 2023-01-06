using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.Utility.Extensions;
using UnityEditor;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.StackZones.ComponentsBuilding
{
    public abstract class StackZoneComponentsBuilder : IStackZoneComponentsBuilder
    {
        protected readonly GameObject rootGameObject;

        protected StackZoneSetupData data;
        protected StackZone zone;
        
        private readonly SetupScheme _zoneSchemePrefab;


        protected StackZoneComponentsBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab)
        {
            this.rootGameObject = rootGameObject;
            _zoneSchemePrefab = zoneSchemePrefab;
        }


        public virtual void Build(StackZoneSetupData data, StackZone zone)
        {
            this.data = data;
            this.zone = zone;
        }


        protected void RestoreMissingComponents(Type markerType)
        {
            GameObject obj = rootGameObject.GetChildByMarker(markerType);
            
            IEnumerable<Component> existingComponents = obj.GetComponents<Component>().Except(new[] {obj.transform});
            Component[] missingComponents = _zoneSchemePrefab.GetComponentsOf(markerType)
                .Where(mc => !existingComponents
                    .Select(ec => ec.GetType())
                        .Contains(mc.GetType())).ToArray();
            foreach (Component missingComponent in missingComponents)
            {
#if UNITY_EDITOR
                EditorUtility.CopySerialized(missingComponent,
                    obj.AddComponent(missingComponent.GetType()));
#endif
            }
        }
    }
}