using System.Collections.Generic;
using ColonizationMobileGame.SetupSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour, IConstructable, IItemsAmountDataProvider
        where TUpgradeData : UpgradeData
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        protected UpgradesChain<TUpgradeData> chain;

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


        public void SetItemsAmountData()
        {
            Upgrade<TUpgradeData> current = chain.Current;

            itemsAmountPanelData.SetData(current?.Price.ToItemsCount());
            itemsAmountPanelData.SetIdentifier(current?.Identifier);
            itemsAmountPanelData.SetUnlockable(current);
            
            itemsAmountPanelData.InvokeChanged();
        }
    }
}