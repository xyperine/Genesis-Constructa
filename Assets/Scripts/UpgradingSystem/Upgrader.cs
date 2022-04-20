using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.SetupSystem;
using UnityEngine;

namespace MoonPioneerClone.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour, IConstructable
        where TUpgradeData : UpgradeData
    {
        [SerializeField] protected ItemsConsumer consumer;
        
        protected IEnumerable<IUpgradeable<TUpgradeData>> upgradeables;
        protected UpgradesStatusTracker<TUpgradeData> upgradesTracker;


        public abstract void Construct(IConstructData data);


        public void Setup(UpgradesChainSO<TUpgradeData> upgradesChain, IEnumerable<IUpgradeable<TUpgradeData>> upgradeables)
        {
            this.upgradeables = upgradeables;
            upgradesTracker = upgradesChain.Upgrades;

            upgradesTracker.Purchased += Upgrade;
            
            consumer.Setup(upgradesChain.RequirementsChain);
        }


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