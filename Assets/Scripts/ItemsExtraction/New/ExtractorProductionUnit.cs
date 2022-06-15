using System.Collections;
using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
using ColonizationMobileGame.ItemsExtraction.Upgrading;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ObjectPooling;
using ColonizationMobileGame.UpgradingSystem;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.New
{
    public class ExtractorProductionUnit : MonoBehaviour, IUpgradeable<ExtractorUpgradeData>
    {
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        
        [SerializeField] private ExtractorProductionRateSO productionRateSO;
        [SerializeField] private ItemType itemType;

        [SerializeField] private StackZone productionStackZone;
        [SerializeField] private ItemsPool itemsPool;

        [SerializeField] private bool produce = true;
        [SerializeField] private ExtractorProductionWorkflow workflow;
        

        private IEnumerator _productionCoroutine;
        private float _itemsPerSecond;

        public float ItemsPerSecond => _itemsPerSecond;
        public bool CanProduce => productionStackZone.CanTakeMore;


        private void Awake()
        {
            _itemsPerSecond = productionRateSO.ItemsPerSecond;
        }


        private void OnEnable()
        {
            if (conditionsUnit.ProductionConditionsMet)
            {
                StartProduction();
            }
            
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
            conditionsUnit.ConditionsChanged += OnProductionConditionsChanged;
        }


        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
            conditionsUnit.ConditionsChanged -= OnProductionConditionsChanged;
        }


        private void OnConditionsChanged()
        {
            if (conditionsUnit.ProductionConditionsMet)
            {
                StartProduction();
                return;
            }
            
            StopProduction();
        }
        
        
        private void OnProductionConditionsChanged()
        {
            
        }


        public void StartProduction()
        {
            if (workflow == ExtractorProductionWorkflow.Conversion)
            {
                return;
            }
            
            if (!produce)
            {
                return;
            }
            
            if (_productionCoroutine != null)
            {
                return;
            }

            _productionCoroutine = ProductionCoroutine();
            StartCoroutine(_productionCoroutine);
        }


        private IEnumerator ProductionCoroutine()
        {
            while (true)
            {
                yield return Helpers.GetWaitForSeconds(1f / _itemsPerSecond);

                ProduceItem();
            }
        }


        public void ProduceItem()
        {
            StackZoneItem item = itemsPool.Get(itemType, transform.position);
            productionStackZone.Add(item);
        }


        public void StopProduction()
        {
            if (workflow == ExtractorProductionWorkflow.Conversion)
            {
                return;
            }
            
            if (_productionCoroutine == null)
            {
                return;
            }

            StopCoroutine(_productionCoroutine);
            _productionCoroutine = null;
        }


        public void Upgrade(ExtractorUpgradeData data)
        {
            _itemsPerSecond = data.ItemsPerSecond;
        }
    }
}