using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions;
using UnityEngine;

namespace MoonPioneerClone.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour
        where TUpgradeData : UpgradeData
    {
        private UpgradesStatusTracker<TUpgradeData> _upgradesTracker;
        private IEnumerable<IUpgradeable<TUpgradeData>> _upgradeables;


        public void Setup(UpgradesChainSO<TUpgradeData> upgradesChain, IEnumerable<IUpgradeable<TUpgradeData>> upgradeables)
        {
            _upgradeables = upgradeables;
            _upgradesTracker = upgradesChain.Upgrades;

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