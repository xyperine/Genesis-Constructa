using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.SetupSystem.Upgrader.Extractors;
using ColonizationMobileGame.UpgradingSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.Extractors
{
    public class ExtractorSetup : MonoBehaviour
    {
        [SerializeField] private StackZone productionZone;
        [SerializeField] private ExtractorProductionUnit productionUnit;
        [SerializeField] private bool upgradeable;
        [SerializeField, ShowIf(nameof(upgradeable))] private ExtractorUpgraderSetup upgraderSetup;
        [SerializeField, ShowIf(nameof(upgradeable))] private ExtractorUpgradesChainSO upgradesChain;
        [SerializeField, ShowIf(nameof(upgradeable)), Range(1f, 10f)] private float upgraderColliderRadius;
        
        
        private void OnEnable()
        {
            if (upgradeable)
            {
                ExtractorUpgraderSetupData data = new ExtractorUpgraderSetupData(upgradesChain,
                    new IUpgradeable<ExtractorUpgradeData>[] {productionZone, productionUnit}, upgraderColliderRadius);
            
                upgraderSetup.SetData(data); 
            }
        }
    }
}