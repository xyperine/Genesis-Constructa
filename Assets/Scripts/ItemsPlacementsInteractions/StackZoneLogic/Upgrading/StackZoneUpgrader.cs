using MoonPioneerClone.SetupSystem;
using MoonPioneerClone.SetupSystem.StackZones;
using MoonPioneerClone.UpgradingSystem;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
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
            upgradesTracker = zoneUpgraderData.Chain.Upgrades;
            
            upgradesTracker.Purchased += Upgrade;
        }
    }
}