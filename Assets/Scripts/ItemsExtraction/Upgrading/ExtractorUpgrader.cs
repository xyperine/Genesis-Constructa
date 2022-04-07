using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
{
    public sealed class ExtractorUpgrader : Upgrader
    {
        [SerializeField] private ExtractorUpgradesSO upgrades;
        [SerializeField] private ProductionStackZoneUpgrader productionZoneUpgrader;
        [SerializeField] private ExtractorProductionRateSO productionRate;


        protected override void Upgrade()
        {
            productionRate.Upgrade(upgrades.ItemsPerSecond);
            productionZoneUpgrader.Upgrade(upgrades.Capacity);
            
            upgrades.Upgrade();
        }
    }
}