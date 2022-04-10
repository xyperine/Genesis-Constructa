using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<StackZoneItem>
    {
        [SerializeField] private PickUpStackZoneBehaviour pickUpBehaviour;


        protected override void InteractWith(StackZoneItem item)
        {
            if (item.Zone != null && !establisher.CanPickUpFrom(item.Zone))
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

            pickUpBehaviour.PickUpItem(item);
        }
    }
}