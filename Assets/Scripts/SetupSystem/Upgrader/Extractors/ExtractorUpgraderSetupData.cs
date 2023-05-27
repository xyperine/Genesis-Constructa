using System;
using System.Collections.Generic;
using GenesisConstructa.ItemsExtraction.Upgrading;
using GenesisConstructa.UpgradingSystem;

namespace GenesisConstructa.SetupSystem.Upgrader.Extractors
{
    [Serializable]
    public class ExtractorUpgraderSetupData : UpgraderSetupData<IUpgradeable<ExtractorUpgradeData>, ExtractorUpgradesChainSO, ExtractorUpgradeData>
    {
        public ExtractorUpgraderSetupData(ExtractorUpgradesChainSO chainSO, IEnumerable<IUpgradeable<ExtractorUpgradeData>> upgradeables) 
            : base(chainSO, upgradeables)
        {
        }
    }
}