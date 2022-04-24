using System;
using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;

namespace MoonPioneerClone.SetupSystem.Upgrader.StackZones
{
    [Serializable]
    public class StackZoneUpgraderSetupData : UpgraderSetupData<StackZone, StackZoneUpgradesChainSO, StackZoneUpgradeData>
    {
        public StackZoneUpgraderSetupData(StackZoneUpgradesChainSO chain, IEnumerable<StackZone> upgradeables, float colliderRadius) 
            : base(chain, upgradeables, colliderRadius)
        {
        }
    }
}