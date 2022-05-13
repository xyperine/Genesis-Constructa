using MoonPioneerClone.SetupSystem;
using MoonPioneerClone.SetupSystem.Upgrader.Extractors;
using MoonPioneerClone.UpgradingSystem;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
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