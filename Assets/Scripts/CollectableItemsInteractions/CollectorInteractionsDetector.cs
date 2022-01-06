using MoonPioneerClone.Utility.Exceptions;
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
                throw new NoCollectorInteractableAttachedException(other.gameObject);
            }
            
            interactable.Interact(_collector);
        }
    }
}