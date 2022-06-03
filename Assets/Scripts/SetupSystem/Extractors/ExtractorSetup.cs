using MoonPioneerClone.ItemsExtraction;
using MoonPioneerClone.ItemsExtraction.Upgrading;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.SetupSystem.Upgrader.Extractors;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Extractors
{
    public class ExtractorSetup : MonoBehaviour
    {
        [SerializeField] private ExtractorUpgraderSetup upgraderSetup;
        [SerializeField] private ExtractorUpgradesChainSO upgradesChain;
        [SerializeField] private StackZone productionZone;
        [SerializeField] private ExtractorProductionUnit productionUnit;
        [SerializeField, Range(1f, 10f)] private float upgraderColliderRadius;
        

#if !UNITY_EDITOR
        private void OnEnable()
        {
            ExtractorUpgraderSetupData data = new ExtractorUpgraderSetupData(upgradesChain,
                new IUpgradeable<ExtractorUpgradeData>[] {productionZone, productionUnit}, upgraderColliderRadius);
            
            upgraderSetup.SetData(data);
        }
#endif
        
        
        private void OnValidate()
        {
            ExtractorUpgraderSetupData data = new ExtractorUpgraderSetupData(upgradesChain,
                new IUpgradeable<ExtractorUpgradeData>[] {productionZone, productionUnit}, upgraderColliderRadius);
            
            upgraderSetup.SetData(data);
        }
    }
}