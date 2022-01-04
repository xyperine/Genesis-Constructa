using MoonPioneerClone.Utility.Exceptions;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    public class CollectorInteractionsDetector : MonoBehaviour
    {
        [SerializeField] private Collector collector;
        

        private void OnTriggerEnter(Collider other)
        {
            ICollectorInteractable interactable;

            if (!other.TryGetComponent(out interactable))
            {
                throw new NoCollectorInteractableAttachedException(other.gameObject);
            }
            
            interactable.Interact(collector);
        }
    }
}