using GenesisConstructa.ItemsExtraction;
using GenesisConstructa.ItemsExtraction.Upgrading;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.SetupSystem.Upgrader.Extractors;
using GenesisConstructa.UpgradingSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.Extractors
{
    public class ExtractorSetup : MonoBehaviour
    {
        [SerializeField] private StackZone productionZone;
        [SerializeField] private ExtractorProductionUnit productionUnit;
        [SerializeField] private bool upgradeable;
        [SerializeField, ShowIf(nameof(upgradeable))] private ExtractorUpgraderSetup upgraderSetup;
        [SerializeField, ShowIf(nameof(upgradeable))] private ExtractorUpgradesChainSO upgradesChain;


        private void OnEnable()
        {
            if (!upgradeable)
            {
                return;
            }

            ExtractorUpgraderSetupData data = new ExtractorUpgraderSetupData(upgradesChain,
                new IUpgradeable<ExtractorUpgradeData>[] {productionZone, productionUnit});
            
            upgraderSetup.SetData(data);
        }
    }
}