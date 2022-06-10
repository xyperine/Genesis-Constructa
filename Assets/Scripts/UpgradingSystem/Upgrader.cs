using System.Collections.Generic;
using ColonizationMobileGame.SetupSystem;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour, IConstructable
        where TUpgradeData : UpgradeData
    {
        protected IEnumerable<IUpgradeable<TUpgradeData>> upgradeables;
        protected UpgradesStatusTracker<TUpgradeData> upgradesTracker;


        public abstract void Construct(IConstructData data);


        protected void Upgrade(TUpgradeData data)
        {
            foreach (IUpgradeable<TUpgradeData> upgradeable in upgradeables)
            {
                upgradeable.Upgrade(data);
            }
        }
        
        
        private void OnDisable()
        {
            upgradesTracker.Purchased -= Upgrade;
        }
    }
}