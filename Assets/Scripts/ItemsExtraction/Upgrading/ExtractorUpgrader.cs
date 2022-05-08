using MoonPioneerClone.SetupSystem;
using MoonPioneerClone.SetupSystem.StackZones;
using MoonPioneerClone.SetupSystem.Upgrader.Extractors;
using MoonPioneerClone.UpgradingSystem;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
{
    public class ExtractorUpgrader : Upgrader<ExtractorUpgradeData>
    {
        public override void Construct(IConstructData data)
        {
            if (data is not ExtractorUpgraderSetupData zoneUpgraderData)
            {
                return;
            }

            upgradeables = zoneUpgraderData.Upgradeables;
            upgradesTracker = zoneUpgraderData.Chain.Upgrades;
            
            upgradesTracker.Purchased += Upgrade;
        }
    }
}