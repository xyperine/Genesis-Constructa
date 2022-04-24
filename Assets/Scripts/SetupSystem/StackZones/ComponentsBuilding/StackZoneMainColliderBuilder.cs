using System.Linq;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.SetupSystem.StackZones.Markers;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneMainColliderBuilder : StackZoneComponentsBuilder
    {
        private GameObject _objForCollider;


        public StackZoneMainColliderBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Setup(StackZoneSetupData data, StackZone zone)
        {
            base.Setup(data, zone);

            RestoreMissingComponents(typeof(MainColliderSetupMarker));

            _objForCollider = rootGameObject.GetGameObjectByMarker(typeof(MainColliderSetupMarker));
            
            SetupCollider();
            SetupInteractionTargetReference();
        }


        private void SetupCollider()
        {
            Collider[] colliders = _objForCollider.GetComponents<Collider>();
            Collider collider = colliders.SingleOrDefault(c => c.GetType() == data.ColliderData.SelectedColliderType);

            if (!collider)
            {
                Debug.LogError("No colliders available!");
                return;
            }
            
            collider.isTrigger = true;
            
            foreach (Collider wrongCollider in colliders.Except(new[] {collider}))
            {
                Object.DestroyImmediate(wrongCollider);
            }
        }


        private void SetupInteractionTargetReference()
        {
            InteractionTargetReference targetReference = _objForCollider.GetComponent<InteractionTargetReference>();
            targetReference.Setup(zone);
        }
    }
}