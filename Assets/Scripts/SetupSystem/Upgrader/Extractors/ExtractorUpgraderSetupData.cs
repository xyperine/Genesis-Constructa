using System;
using System.Collections.Generic;
using MoonPioneerClone.ItemsExtraction.Upgrading;
using MoonPioneerClone.UpgradingSystem;

namespace MoonPioneerClone.SetupSystem.Upgrader.Extractors
{
    [Serializable]
    public class ExtractorUpgraderSetupData : UpgraderSetupData<IUpgradeable<ExtractorUpgradeData>, ExtractorUpgradesChainSO, ExtractorUpgradeData>
    {
        public ExtractorUpgraderSetupData(ExtractorUpgradesChainSO chain, IEnumerable<IUpgradeable<ExtractorUpgradeData>> upgradeables, float colliderRadius) 
            : base(chain, upgradeables, colliderRadius)
        {
        }
    }
}