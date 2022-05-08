using MoonPioneerClone.ItemsExtraction.Upgrading;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.SetupSystem.Upgrader.Extractors;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorSetup : MonoBehaviour
    {
        [SerializeField] private ExtractorUpgraderSetup upgraderSetup;
        [SerializeField] private ExtractorUpgradesChainSO upgradesChain;
        [SerializeField] private StackZone productionZone;
        [SerializeField] private ExtractorProductionRateSO productionRate;
        [SerializeField, Range(1f, 10f)] private float upgraderColliderRadius;
        

        private void OnValidate()
        {
            ExtractorUpgraderSetupData data = new ExtractorUpgraderSetupData(upgradesChain,
                new IUpgradeable<ExtractorUpgradeData>[] {productionZone, productionRate}, upgraderColliderRadius);
            
            upgraderSetup.SetData(data);
        }
    }
}