using ColonizationMobileGame.SetupSystem;
using ColonizationMobileGame.SetupSystem.Upgrader.Extractors;
using ColonizationMobileGame.UpgradingSystem;

namespace ColonizationMobileGame.ItemsExtraction.Upgrading
{
    public class ExtractorUpgrader : Upgrader<ExtractorUpgradeData>
    {
        public override void Construct(IConstructData data)
        {
            if (data is not ExtractorUpgraderSetupData extractorUpgraderData)
            {
                return;
            }

            upgradeables = extractorUpgraderData.Upgradeables;
            upgradesTracker = extractorUpgraderData.Chain.UpgradesStatusTracker;

            upgradesTracker.Purchased += Upgrade;
        }
    }
}