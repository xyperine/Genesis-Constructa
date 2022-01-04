using MoonPioneerClone.Utility.Exceptions;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    public class StackZoneItem : MonoBehaviour, ICollectorInteractable
    {
        [SerializeField] private ResourceType type;

        private ItemsStackZone _zone;
        
        public ResourceType Type => type;


        public void SetZone(ItemsStackZone zone)
        {
            if (zone == _zone)
            {
                throw new SameStackZoneException();
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

            _zone.TryTransferItemTo(this, potentialNewZone);
        }
    }
}