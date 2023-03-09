using System;
using System.Linq;
using ColonizationMobileGame.BuildSystem;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UI.ArrowPointers.Target;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    public class UnlockStation : MonoBehaviour, IItemsAmountDataProvider, ISceneSaveable, IArrowPointerTargetProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private UnlocksChainSO chainSO;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        public int LoadingOrder => -1;
        public PermanentGuid Guid => guid;
        
        public event Action<Transform> TargetReady;
        public event Action<StructureIdentifier> Unlocked;


        private void Start()
        {
            consumer.Setup(chainSO.RequirementsChain);
            consumer.Consumed += SetItemsAmountData;
            
            chainSO.RequirementsChain.ChangingBlock += SetItemsAmountData;
            chainSO.RequirementsChain.ChangingBlock += InvokeUnlocked;
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


        private void InvokeUnlocked()
        {
            Unlocked?.Invoke(chainSO.Current.Identifier);
        }


        private void InvokeTargetReady()
        {
            Transform[] transforms = FindObjectsOfType<Builder>()
                .Where(b => b.StructureType == chainSO.Current.Identifier.StructureType).Select(b => b.transform).ToArray();
            
            foreach (Transform targetTransform in transforms)
            {
                TargetReady?.Invoke(targetTransform);
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