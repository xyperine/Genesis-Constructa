using System;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.Upgrader.StackZones
{
    [Serializable]
    public class StackZoneUpgraderComponentsBuilder : UpgraderComponentsBuilder<StackZoneUpgraderSetupData>
    {
        [SerializeField] private StackZoneUpgrader upgrader;
        
        
        public override void SetupCollider()
        {
            collider.radius = setupData.ColliderRadius;
        }


        protected override void SetupItemsConsumer()
        {
            consumer.Setup(setupData.Chain);
        }


        protected override void SetupUpgrader()
        {
            upgrader.Construct(setupData);
        }
    }
}