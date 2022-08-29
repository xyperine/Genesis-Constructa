using System;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    public class UnlockStation : MonoBehaviour, IItemsAmountDataProvider, ISaveableWithGuid
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private UnlocksChainSO chainSO;

        [SerializeField] private ScoreModifier scoreModifier;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        public PermanentGuid Guid => guid;


        private void Start()
        {
            consumer.Setup(chainSO.RequirementsChain);
            consumer.Consumed += SetItemsAmountData;
            
            chainSO.RequirementsChain.ChangingBlock += SetItemsAmountData;
            chainSO.RequirementsChain.ChangingBlock += AddScore;

            SetItemsAmountData();
        }
        

        public void SetItemsAmountData()
        {
            Unlock current = chainSO.Current;

            itemsAmountPanelData.SetData(current?.Price.ToItemsAmount());
            itemsAmountPanelData.SetIdentifier(current?.Identifier);

            itemsAmountPanelData.InvokeChanged();
        }


        private void AddScore()
        {
            scoreModifier.Add(chainSO.Current.Identifier.StructureType);
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