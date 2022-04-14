using MoonPioneerClone.UpgradesSystem.Upgrading;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic
{
    public class StackZoneSetup : MonoBehaviour
    {
        [SerializeField] private StackZoneUpgrader upgrader;
        [SerializeField] private StackZoneUpgradesChainSO upgradesChain;


        private void Start()
        {
            upgrader.Setup(upgradesChain.Upgrades, GetComponent<StackZone>());
        }
    }
}