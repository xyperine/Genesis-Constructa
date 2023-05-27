using GenesisConstructa.SetupSystem;
using GenesisConstructa.SetupSystem.Upgrader.StackZones;
using GenesisConstructa.UpgradingSystem;

namespace GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    public class StackZoneUpgrader : Upgrader<StackZoneUpgradeData>
    {
        public override void Construct(IConstructData data)
        {
            if (data is not StackZoneUpgraderSetupData zoneUpgraderData)
            {
                return;
            }

            upgradeables = zoneUpgraderData.Upgradeables;
            upgradesTracker = zoneUpgraderData.Chain.UpgradesStatusTracker;
            
            upgradesTracker.Purchased += Upgrade;
        }
    }
}