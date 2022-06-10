using System;
using System.Collections.Generic;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;

namespace ColonizationMobileGame.SetupSystem.Upgrader.StackZones
{
    [Serializable]
    public class StackZoneUpgraderSetupData : UpgraderSetupData<StackZone, StackZoneUpgradesChainSO, StackZoneUpgradeData>
    {
        public StackZoneUpgraderSetupData(StackZoneUpgradesChainSO chainSO, IEnumerable<StackZone> upgradeables, float colliderRadius) 
            : base(chainSO, upgradeables, colliderRadius)
        {
        }
    }
}