namespace MoonPioneerClone.ItemsInteractions.PickUp
{
    public class PickUpsInteractor : StackZoneInteractor<StackZoneItem, PickUpInteractionsHandler>
    {
        protected override void InteractWith(StackZoneItem item)
        {
            handler.PickUpItem(item);
        }
    }
}