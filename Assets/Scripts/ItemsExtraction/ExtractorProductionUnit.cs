using System.Collections;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorProductionUnit : MonoBehaviour
    {
        [Tooltip("Units per second")]
        [SerializeField, Min(0.01f)] private float productionRate;
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
                yield return new WaitForSeconds(1f / productionRate);

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
    }
}