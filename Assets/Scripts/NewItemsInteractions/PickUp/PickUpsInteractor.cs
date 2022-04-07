using System.Collections.Generic;
using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<StackZoneItem>
    {
        [SerializeField] private PickUpStackZoneBehaviour pickUpBehaviour;


        protected override void InteractWith(StackZoneItem item)
        {
            if (item.Zone != null && !resolver.CanPickUpFrom(item.Zone))
            {
                return;
            }
            
            PickUpItem(item);
        }
        
        
        private void PickUpItem(StackZoneItem item)
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
                pickUpBehaviour.Add(item);
                return;
            }
            
            if (!validator.CanTakeFrom(itemZone))
            {
                return;
            }
            
            pickUpBehaviour.PickUpItem(item);
        }
    }
}