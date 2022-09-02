using System;
using System.Collections.Generic;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.SetupSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UpgradingSystem
{
    public abstract class Upgrader<TUpgradeData> : MonoBehaviour, IConstructable, IItemsAmountDataProvider, ISaveableWithGuid
        where TUpgradeData : UpgradeData
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        
        [SerializeField, HideInInspector] private PermanentGuid guid;

        protected UpgradesChain<TUpgradeData> chain;
        protected object chainSaveData;

        protected IEnumerable<IUpgradeable<TUpgradeData>> upgradeables;
        protected UpgradesStatusTracker<TUpgradeData> upgradesTracker;
        
        public int Level { get; private set; }

        public SaveableType SaveableType => SaveableType.Other;
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
            return new SaveData()
            {
                UpgradesChainData = chain.Save(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            if (chain == null)
            {
                chainSaveData = saveData.UpgradesChainData;
                return;
            }
            
            chain.Load(saveData.UpgradesChainData);
            
            SetItemsAmountData();
        }
        

        [Serializable]
        private struct SaveData
        {
            public object UpgradesChainData { get; set; }
        }
    }
}