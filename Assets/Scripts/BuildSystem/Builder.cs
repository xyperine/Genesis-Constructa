using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsPlacementsInteractions;
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


        private void Awake()
        {
            _buildData = buildDataSO.Current;
            
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
            consumer.Setup(buildDataSO.RequirementsChain);
            _buildData.Price.Fulfilled += Build;
        }


        private void Build()
        {
            GameObject structure = Instantiate(_buildData.StructurePrefab, transform.position, Quaternion.identity, structuresParent);
            structure.GetComponentInChildren<ExtractorProductionUnit>().SetPool(itemsPool);
            
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
            itemsAmountPanelData.SetData(_buildData.Price.ToItemsCount());
            itemsAmountPanelData.SetIdentifier(_buildData.Identifier);
            itemsAmountPanelData.SetUnlockable(_buildData);
            
            itemsAmountPanelData.InvokeChanged();
        }
    }
}