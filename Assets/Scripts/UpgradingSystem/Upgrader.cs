using System;
using System.Collections.Generic;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.SetupSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour, IConstructable, IItemsAmountDataProvider, ISceneSaveable
        where TUpgradeData : UpgradeData
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        protected UpgradesChain<TUpgradeData> chain;
        protected object chainSaveData;

        protected IEnumerable<IUpgradeable<TUpgradeData>> upgradeables;
        protected UpgradesStatusTracker<TUpgradeData> upgradesTracker;
        
        public int Level { get; private set; }

        public int LoadingOrder => 0;
        public PermanentGuid Guid => guid;


        public abstract void Construct(IConstructData data);


        protected void Upgrade(TUpgradeData data)
        {
            foreach (IUpgradeable<TUpgradeData> upgradeable in upgradeables)
            {
                upgradeable.Upgrade(data);
            }

            Level++;
        }
        
        
        private void OnDisable()
        {
            upgradesTracker.Purchased -= Upgrade;
        }


        public void SetItemsAmountData()
        {
            Upgrade<TUpgradeData> current = chain.Current;

            itemsAmountPanelData.SetData(current?.Price.ToItemsAmount());
            itemsAmountPanelData.SetIdentifier(current?.Identifier);
            itemsAmountPanelData.SetUnlockable(current);
            
            itemsAmountPanelData.InvokeChanged();
        }


        public object Save()
        {
            if (chain == null)
            {
                return null;
            }
            
            return new SaveData()
            {
                UpgradesChainData = chain.Save(),
            };
        }


        public void Load(object data)
        {
            if (data == null)
            {
                return;
            }
            
            SaveData saveData = (SaveData) data;

            chainSaveData = saveData.UpgradesChainData;
            
            if (chain == null)
            {
                return;
            }
            
            chain.Load(chainSaveData);
            
            SetItemsAmountData();
        }
        

        [Serializable]
        private struct SaveData
        {
            public object UpgradesChainData { get; set; }
        }
    }
}