using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    [RequireComponent(typeof(Collector))]
    public sealed class CollectorInteractionsDetector : MonoBehaviour
    {
        private Collector _collector;


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            _collector = GetComponent<Collector>();
        }


        private void OnTriggerEnter(Collider other)
        {
            ICollectorInteractable interactable;

            if (!other.TryGetComponent(out interactable))
            {
                return;
            }
            
            interactable.Interact(_collector);
        }
    }
}