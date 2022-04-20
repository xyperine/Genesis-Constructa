using System;
using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using MoonPioneerClone.SetupSystem.Upgrader;

namespace MoonPioneerClone.SetupSystem.StackZones
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