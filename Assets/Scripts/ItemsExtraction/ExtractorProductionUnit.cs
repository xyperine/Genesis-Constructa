using System.Collections;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorProductionUnit : MonoBehaviour
    {
        [Tooltip("Items per second")]
        [SerializeField, Min(0.01f)] private float productionRate;
        [SerializeField] private StackZoneItem product;

        [SerializeField] private StackZone productionStackZone;

        private WaitForSeconds _waitForProductionInterval;
        private IEnumerator _productionCoroutine;


        private void Awake()
        {
            _waitForProductionInterval = new WaitForSeconds(1f / productionRate);
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
                yield return _waitForProductionInterval;

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
            StopCoroutine(_productionCoroutine);
            _productionCoroutine = null;
        }


        public void IncreaseProductionRate()
        {
            productionRate *= 2f;
            _waitForProductionInterval = new WaitForSeconds(1f / productionRate);
        }
    }
}