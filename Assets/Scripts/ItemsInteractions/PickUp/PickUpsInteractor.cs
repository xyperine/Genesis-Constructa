namespace MoonPioneerClone.ItemsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<ZoneItem>
    {
        protected override void InteractWith(ZoneItem item)
        {
            handler.PickUpItem(item);
        }
    }
}