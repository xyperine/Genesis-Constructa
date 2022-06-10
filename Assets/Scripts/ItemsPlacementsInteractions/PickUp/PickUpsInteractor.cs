using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<StackZoneItem>
    {
        [SerializeField] private PickUpStackZoneBehaviour pickUpBehaviour;


        public void Setup(InteractionsEstablisher establisher, PickUpStackZoneBehaviour pickUpBehaviour)
        {
            this.establisher = establisher;
            this.pickUpBehaviour = pickUpBehaviour;
        }
        
        
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