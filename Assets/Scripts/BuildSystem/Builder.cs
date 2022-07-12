using ColonizationMobileGame.ItemsExtraction;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ObjectPooling;
using UnityEngine;

namespace ColonizationMobileGame.BuildSystem
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private BuildDataSO buildDataSO;
        [SerializeField] private Transform structuresParent;
        [SerializeField] private ItemsPool itemsPool;
        [SerializeField] private ItemsConsumer consumer;
        
        private BuildData _buildData;


        private void Awake()
        {
            _buildData = buildDataSO.Current;
            
            SetupPrice();

            if (!_buildData.Locked)
            {
                return;
            }

            _buildData.Unlocked += OnUnlocked;
            gameObject.SetActive(false);
        }
        
        
        private void SetupPrice()
        {
            consumer.Setup(buildDataSO);
            _buildData.Price.Fulfilled += Build;
        }


        private void Build()
        {
            GameObject structure = Instantiate(_buildData.StructurePrefab, transform.position, Quaternion.identity, structuresParent);
            structure.GetComponentInChildren<ExtractorProductionUnit>().SetPool(itemsPool);
            
            Destroy(gameObject);
        }


        private void OnUnlocked()
        {
            gameObject.SetActive(true);
        }
    }
}