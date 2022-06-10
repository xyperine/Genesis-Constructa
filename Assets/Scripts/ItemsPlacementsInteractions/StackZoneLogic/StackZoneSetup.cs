using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic
{
    public class StackZoneSetup : MonoBehaviour
    {
        [SerializeField] private StackZoneUpgrader upgrader;
        [SerializeField] private StackZoneUpgradesChainSO upgradesChain;
        [SerializeField] private ItemsConsumer consumer;


        private void Start()
        {
            //upgrader.Setup(upgradesChain.Upgrades, new []{GetComponent<StackZone>()});
            //consumer.Setup(upgradesChain.RequirementsChain);
        }
    }
}