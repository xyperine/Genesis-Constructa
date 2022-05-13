using System.Collections;
using MoonPioneerClone.ItemsExtraction.Upgrading;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.UpgradingSystem;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public sealed class ExtractorProductionUnit : MonoBehaviour, IUpgradeable<ExtractorUpgradeData>
    {
        [SerializeField] private ExtractorProductionRateSO productionRateSO;
        [SerializeField] private StackZoneItem product;

        [SerializeField] private StackZone productionStackZone;

        private IEnumerator _productionCoroutine;
        private float _itemsPerSecond;


        private void Awake()
        {
            _itemsPerSecond = productionRateSO.ItemsPerSecond;
        }


        public void StartProduction()
        {
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


        private void ProduceItem()
        {
            StackZoneItem item = Instantiate(product, transform.position, Quaternion.identity);
            productionStackZone.Add(item);
        }


        public void StopProduction()
        {
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
