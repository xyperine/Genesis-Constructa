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

        public float ItemsPerSecond { get; private set; }

        public bool CanProduce => productionStackZone.CanTakeMore;


        private void Awake()
        {
            ItemsPerSecond = productionRateSO.ItemsPerSecond;
        }


        private void OnEnable()
        {
            if (conditionsUnit.ProductionConditionsMet)
            {
                StartProduction();
            }
            
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void StartProduction()
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
                yield return Helpers.GetWaitForSeconds(1f / ItemsPerSecond);

                ProduceItem();
            }
        }


        public void ProduceItem()
        {
            StackZoneItem item = itemsPool.Get(itemType, transform.position);
            productionStackZone.Add(item);
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


        private void StopProduction()
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


        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
        }


        public void Upgrade(ExtractorUpgradeData data)
        {
            ItemsPerSecond = data.ItemsPerSecond;
        }
    }
}