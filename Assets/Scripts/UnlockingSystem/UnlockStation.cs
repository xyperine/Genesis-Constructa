using System;
using System.Linq;
using ColonizationMobileGame.BuildSystem;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.UI.ArrowPointers;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    public class UnlockStation : MonoBehaviour, IItemsAmountDataProvider, ISceneSaveable, IArrowPointerTargetProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private UnlocksChainSO chainSO;

        [SerializeField] private ScoreModifier scoreModifier;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        public int LoadingOrder => -1;
        public PermanentGuid Guid => guid;

        public event Action<Transform, ArrowPointerTargetCondition> TargetReady;


        private void Start()
        {
            consumer.Setup(chainSO.RequirementsChain);
            consumer.Consumed += SetItemsAmountData;
            
            chainSO.RequirementsChain.ChangingBlock += SetItemsAmountData;
            chainSO.RequirementsChain.ChangingBlock += AddScore;
            chainSO.RequirementsChain.ChangingBlock += InvokeTargetReady;

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


        private void InvokeTargetReady()
        {
            Transform[] transforms = FindObjectsOfType<Builder>()
                .Where(b => b.StructureType == chainSO.Current.Identifier.StructureType).Select(b => b.transform).ToArray();
            
            foreach (Transform targetTransform in transforms)
            {
                TargetReady?.Invoke(targetTransform, new ArrowPointerTargetCondition());
            }
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