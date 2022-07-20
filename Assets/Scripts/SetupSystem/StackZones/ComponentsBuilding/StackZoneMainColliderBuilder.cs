using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.SetupSystem.StackZones.Markers;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneMainColliderBuilder : StackZoneComponentsBuilder
    {
        private GameObject _objForCollider;


        public StackZoneMainColliderBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Build(StackZoneSetupData data, StackZone zone)
        {
            base.Build(data, zone);

            RestoreMissingComponents(typeof(MainColliderSetupMarker));

            _objForCollider = rootGameObject.GetChildByMarker(typeof(MainColliderSetupMarker));
            
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