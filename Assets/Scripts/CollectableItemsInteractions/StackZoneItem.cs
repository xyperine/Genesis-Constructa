using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    public sealed class StackZoneItem : MonoBehaviour, ICollectorInteractable
    {
        [SerializeField] private ResourceType type;

        private ItemsStackZone _zone;
        
        public ResourceType Type => type;


        public void SetZone(ItemsStackZone zone)
        {
            if (_zone == zone)
            {
                return;
            }
            
            _zone = zone;
        }


        public void Interact(Collector collector)
        {
            ItemsStackZone potentialNewZone = collector as ItemsStackZone;
            
            if (!potentialNewZone)
            {
                return;
            }
            
            if (!_zone)
            {
                potentialNewZone.TryAdd(this);
                return;
            }

            _zone.TryTransferItemTo(potentialNewZone, this);
        }
    }
}