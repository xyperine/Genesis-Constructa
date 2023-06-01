using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;

namespace GenesisConstructa.ItemsPlacementsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<StackZoneItem, PickUpStackZoneBehaviour>
    {
        protected override bool CanScan => behaviour.EnoughSpace;
        protected override bool IgnoreYPosition => true;


        protected override void InteractWith(StackZoneItem item)
        {
            if (item.LockedForPlayer || item.Zone != null && !establisher.CanPickUpFrom(item.Zone))
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