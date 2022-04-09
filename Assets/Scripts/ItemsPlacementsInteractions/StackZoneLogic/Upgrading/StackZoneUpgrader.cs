using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    public sealed class StackZoneUpgrader : Upgrader
    {
        [SerializeField] private StackZoneUpgradesSO upgrades;
        [SerializeField] private StackZone stackZone;


        protected override void Upgrade()
        {
            upgrades.Upgrade();
            stackZone.Upgrade(upgrades.Capacity);
        }
    }
}