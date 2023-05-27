using System;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.Upgrader.StackZones
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