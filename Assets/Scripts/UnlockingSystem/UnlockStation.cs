using System;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.SaveLoadSystem;
using GenesisConstructa.Structures;
using GenesisConstructa.UI.ItemsAmount.Data;
using UnityEngine;

namespace GenesisConstructa.UnlockingSystem
{
    public class UnlockStation : MonoBehaviour, IItemsAmountDataProvider, ISceneSaveable
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private UnlocksChainSO chainSO;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        public int LoadingOrder => -1;
        public PermanentGuid Guid => guid;
        
        public event Action<StructureIdentifier> Unlocked;


        private void Start()
        {
            consumer.Setup(chainSO.RequirementsChain);
            consumer.Consumed += SetItemsAmountData;
            
            chainSO.RequirementsChain.ChangingBlock += SetItemsAmountData;
            chainSO.RequirementsChain.ChangingBlock += InvokeUnlocked;

            SetItemsAmountData();
        }


        public void SetItemsAmountData()
        {
            Unlock current = chainSO.Current;

            itemsAmountPanelData.SetData(current?.Price.ToItemsAmount());
            itemsAmountPanelData.SetIdentifier(current?.Identifier);

            itemsAmountPanelData.InvokeChanged();
        }


        private void InvokeUnlocked()
        {
            Unlocked?.Invoke(chainSO.Current.Identifier);
        }


        public object Save()
        {
            return new SaveData
            {
                UnlocksChainSOSaveData = chainSO.Save(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            chainSO.Load(saveData.UnlocksChainSOSaveData);
            
            SetItemsAmountData();
        }


        [Serializable]
        private struct SaveData
        {
            public object UnlocksChainSOSaveData { get; set; }
        }
    }
}