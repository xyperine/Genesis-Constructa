using System;
using System.Collections.Generic;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;

namespace GenesisConstructa.SetupSystem.Upgrader.StackZones
{
    [Serializable]
    public class StackZoneUpgraderSetupData : UpgraderSetupData<StackZone, StackZoneUpgradesChainSO, StackZoneUpgradeData>
    {
        public StackZoneUpgraderSetupData(StackZoneUpgradesChainSO chainSO, IEnumerable<StackZone> upgradeables) 
            : base(chainSO, upgradeables)
        {
        }
    }
}