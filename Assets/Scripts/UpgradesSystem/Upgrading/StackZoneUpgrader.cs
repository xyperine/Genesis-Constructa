using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.UpgradesSystem.Upgrading
{
    public class StackZoneUpgrader : MonoBehaviour
    {
        private StackZone _stackZone;
        private UpgradesStatusTracker<StackZoneUpgradeData> _upgradesTracker;


        public void Setup(UpgradesStatusTracker<StackZoneUpgradeData> upgradesStatusTracker, StackZone stackZone)
        {
            _upgradesTracker = upgradesStatusTracker;
            _stackZone = stackZone;

            _upgradesTracker.Purchased += _stackZone.Upgrade;
        }


        private void OnDisable()
        {
            _upgradesTracker.Purchased -= _stackZone.Upgrade;
        }
    }
}