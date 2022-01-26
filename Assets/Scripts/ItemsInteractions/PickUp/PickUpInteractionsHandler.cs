using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.PickUp
{
    public sealed class PickUpInteractionsHandler : ItemsInteractionsHandler
    {
        [SerializeField] private PickUpStackZoneBehavior pickUpBehavior;


        public void PickUpItem(ZoneItem item)
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
                pickUpBehavior.Add(item);
                return;
            }
            
            if (!validator.CanTakeFrom(itemZone))
            {
                return;
            }
            
            pickUpBehavior.PickUpItem(item);
        }
    }
}