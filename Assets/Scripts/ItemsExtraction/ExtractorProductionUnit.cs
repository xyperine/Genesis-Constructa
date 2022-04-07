using System.Collections;
using MoonPioneerClone.ItemsInteractions;
using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public sealed class ExtractorProductionUnit : MonoBehaviour
    {
        [SerializeField] private ExtractorProductionRateSO productionRate;
        [SerializeField] private StackZoneItem product;

        [SerializeField] private StackZone productionStackZone;

        private IEnumerator _productionCoroutine;


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
                yield return new WaitForSeconds(1f / productionRate.ItemsPerSecond);

                ProduceItem();
            }
        }


        public void ProduceItem()
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
    }
}
