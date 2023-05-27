using GenesisConstructa.SetupSystem;
using GenesisConstructa.SetupSystem.Upgrader.Extractors;
using GenesisConstructa.UpgradingSystem;

namespace GenesisConstructa.ItemsExtraction.Upgrading
{
    public class ExtractorUpgrader : Upgrader<ExtractorUpgradeData>
    {
        public override void Construct(IConstructData data)
        {
            if (data is not ExtractorUpgraderSetupData extractorUpgraderData)
            {
                return;
            }

            chain = extractorUpgraderData.Chain;

            upgradeables = extractorUpgraderData.Upgradeables;
            upgradesTracker = extractorUpgraderData.Chain.UpgradesStatusTracker;

            upgradesTracker.Purchased += Upgrade;

            if (chainSaveData != null)
            {
                chain.Load(chainSaveData);
            }
            
            SetItemsAmountData();
        }
    }
}