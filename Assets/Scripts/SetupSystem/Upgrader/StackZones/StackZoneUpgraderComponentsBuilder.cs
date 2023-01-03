using System;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.Upgrader.StackZones
{
    [Serializable]
    public class StackZoneUpgraderComponentsBuilder : UpgraderComponentsBuilder<StackZoneUpgraderSetupData>
    {
        [SerializeField] private StackZoneUpgrader upgrader;
        

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