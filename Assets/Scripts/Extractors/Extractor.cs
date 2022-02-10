using System.Collections;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.Extractors
{
    public class Extractor : MonoBehaviour
    {
        [Tooltip("Units per second")]
        [SerializeField, Min(0.01f)] private float productionRate;
        [SerializeField] private StackZoneItem product;

        [SerializeField] private ExtractorAnimator animator;
        [SerializeField] private StackZone stackZone;
        [SerializeField] private ItemsConsumer consumer;

        private IEnumerator _productionCoroutine;


        private void Awake()
        {
            consumer.BlockSatisfied += Upgrade;

            StartProduction();
        }


        public void StartProduction()
        {
            if (_productionCoroutine != null)
            {
                return;
            }

            animator.PlayStartUpAnimation();

            _productionCoroutine = ProductionCoroutine();
            StartCoroutine(_productionCoroutine);
        }


        private IEnumerator ProductionCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / productionRate);

                if (!stackZone.CanTakeMore)
                {
                    continue;
                }

                ProduceItem();
            }
        }


        private void ProduceItem()
        {
            StackZoneItem item = Instantiate(product, transform.position, Quaternion.identity);
            stackZone.Add(item);
        }


        public void StopProduction()
        {
            StopCoroutine(_productionCoroutine);
            _productionCoroutine = null;
        }


        public void Upgrade()
        {
            print("Upgraded");
        }
    }
}