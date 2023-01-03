using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<StackZoneItem, PickUpStackZoneBehaviour>
    {
        protected override bool CanScan => behaviour.EnoughSpace;
        

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

            behaviour.PickUpItem(item);
        }
    }
}