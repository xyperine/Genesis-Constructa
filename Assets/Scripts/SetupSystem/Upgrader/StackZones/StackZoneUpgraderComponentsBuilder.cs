using System;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using MoonPioneerClone.SetupSystem.StackZones;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Upgrader.StackZones
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
            consumer.Setup(setupData.Chain.RequirementsChain);
        }


        protected override void SetupUpgrader()
        {
            upgrader.Construct(setupData);
        }
    }
}