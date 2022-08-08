using System;
using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.ObjectPooling;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    public class Builder : MonoBehaviour, IItemsAmountDataProvider
    {
        [SerializeField] private BuildDataSO buildDataSO;
        [SerializeField] private Transform structuresParent;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        
        private BuildData _buildData;

        public event Action Built;


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
            consumer.Setup(new ItemsRequirementsChain(new[] {_buildData.Price}));
            _buildData.Price.Fulfilled += Build;
        }


        private void Build()
        {
            GameObject structure = Instantiate(_buildData.StructurePrefab, transform.position, Quaternion.identity, structuresParent);
            ExtractorProductionUnit productionUnit = structure.GetComponentInChildren<ExtractorProductionUnit>();
            if (productionUnit)
            {
                productionUnit.SetPool(itemsPool);
            }
            
            Built?.Invoke();

            Invoke(nameof(Deactivate), 1f);
        }


        private void Deactivate()
        {
            Destroy(gameObject);
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
    }
}