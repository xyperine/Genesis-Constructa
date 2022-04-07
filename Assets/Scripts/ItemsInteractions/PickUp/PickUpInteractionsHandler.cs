using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.PickUp
{
    public sealed class PickUpInteractionsHandler : ItemsInteractionsHandler
    {
        [SerializeField] private PickUpStackZoneBehaviour _pickUpBehaviour;


        public void PickUpItem(StackZoneItem item)
        {
            if (item.Moving)
            {
                return;
            }
            
            StackZone itemZone = item.Zone;
            
            if (!validator.ZoneCanTakeItem(item))
            {
                return;
            }

            if (!itemZone)
            {
                _pickUpBehaviour.Add(item);
                return;
            }
            
            if (!validator.CanTakeFrom(itemZone))
            {
                return;
            }
            
            _pickUpBehaviour.PickUpItem(item);
        }
    }
}