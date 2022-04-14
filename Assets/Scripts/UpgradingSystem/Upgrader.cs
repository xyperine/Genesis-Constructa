using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour
        where TUpgradeData : UpgradeData
    {
        private UpgradesStatusTracker<TUpgradeData> _upgradesTracker;
        private IEnumerable<IUpgradeable<TUpgradeData>> _upgradeables;


        public void Setup(UpgradesStatusTracker<TUpgradeData> tracker, IEnumerable<IUpgradeable<TUpgradeData>> upgradeables)
        {
            _upgradeables = upgradeables;
            _upgradesTracker = tracker;

            _upgradesTracker.Purchased += Upgrade;
        }


        private void Upgrade(TUpgradeData data)
        {
            foreach (IUpgradeable<TUpgradeData> upgradeable in _upgradeables)
            {
                upgradeable.Upgrade(data);
            }
        }
        
        
        private void OnDisable()
        {
            _upgradesTracker.Purchased -= Upgrade;
        }
    }
}