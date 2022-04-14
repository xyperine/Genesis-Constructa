using MoonPioneerClone.ItemsExtraction.Upgrading;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorSetup : MonoBehaviour
    {
        [SerializeField] private ExtractorUpgrader upgrader;
        [SerializeField] private ExtractorUpgradesChainSO upgradesChain;
        [SerializeField] private StackZone productionZone;
        [SerializeField] private ExtractorProductionRateSO productionRate;
        [SerializeField] private ItemsConsumer consumer;
        


        private void Start()
        {
            upgrader.Setup(upgradesChain.Upgrades,
                new IUpgradeable<ExtractorUpgradeData>[] {productionZone, productionRate});
            consumer.Setup(upgradesChain.RequirementsChain);
        }
    }
}