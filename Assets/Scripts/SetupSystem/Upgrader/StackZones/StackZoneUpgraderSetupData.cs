using System;
using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;

namespace MoonPioneerClone.SetupSystem.Upgrader.StackZones
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