using System;
using System.Collections.Generic;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.UpgradingSystem;

namespace ColonizationMobileGame.SetupSystem.Upgrader.Extractors
{
    [Serializable]
    public class ExtractorUpgraderSetupData : UpgraderSetupData<IUpgradeable<ExtractorUpgradeData>, ExtractorUpgradesChainSO, ExtractorUpgradeData>
    {
        public ExtractorUpgraderSetupData(ExtractorUpgradesChainSO chainSO, IEnumerable<IUpgradeable<ExtractorUpgradeData>> upgradeables, float colliderRadius) 
            : base(chainSO, upgradeables, colliderRadius)
        {
        }
    }
}