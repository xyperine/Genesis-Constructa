using System;
using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.Level;
using ColonizationMobileGame.ObjectPooling;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    public sealed class Builder : MonoBehaviour, IItemsAmountDataProvider, ILevelDataUser, ISaveableWithGuid
    {
        [SerializeField] private BuildDataSO buildDataSO;
        [SerializeField] private Transform structuresParent;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private LevelData _levelData;

        private BuildData _buildData;

        public PermanentGuid Guid => guid;

        public event Action Built;


        public void SetLevelData(LevelData levelData)
        {
            _levelData = levelData;
        }
        

        private void Awake()
        {
            _buildData = buildDataSO.Data;
            
            SetupPrice();
            SetItemsAmountData();

            consumer.Consumed += SetItemsAmountData;

            if (!_buildData.Locked)
            {
                return;
            }

            _buildData.Unlocked += OnUnlocked;
            gameObject.SetActive(false);
        }
        
        
        private void SetupPrice()
        {
            consumer.Setup(_buildData.Price);
            _buildData.Price.Fulfilled += Build;
        }


        private void Build()
        {
            GameObject structureObject = Instantiate(_buildData.StructurePrefab, transform.position, Quaternion.identity, structuresParent);
            ExtractorProductionUnit productionUnit = structureObject.GetComponentInChildren<ExtractorProductionUnit>();

            if (structureObject.TryGetComponent(out Structure structure))
            {
                structure.Setup(_buildData.Identifier.StructureType, _buildData.MaxLevel);
            }

            if (productionUnit)
            {
                productionUnit.SetPool(itemsPool);
            }
            
            Built?.Invoke();
            
            _levelData.SetStructure(structure);

            Invoke(nameof(Deactivate), 1f);
        }


        private void Deactivate()
        {
            _buildData.Price.Fulfilled -= Build;
            Built = null;
            
            gameObject.SetActive(false);
        }


        private void OnUnlocked()
        {
            gameObject.SetActive(true);
            
            SetItemsAmountData();
        }
        
        
        public void SetItemsAmountData()
        {
            itemsAmountPanelData.SetData(_buildData.Price.ToItemsAmount());
            itemsAmountPanelData.SetIdentifier(_buildData.Identifier);
            itemsAmountPanelData.SetUnlockable(_buildData);
            
            itemsAmountPanelData.InvokeChanged();
        }


        public object Save()
        {
            return new SaveData
            {
                ItemRequirementsBlockData = _buildData.Price.Save(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            _buildData.Price.Load(saveData.ItemRequirementsBlockData);
            
            SetItemsAmountData();
        }

        
        [Serializable]
        private struct SaveData
        {
            public object ItemRequirementsBlockData { get; set; }
        }
    }
}