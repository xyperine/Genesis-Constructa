namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<TransferTargetReference>
    {
        protected override void InteractWith(TransferTargetReference reference)
        {
            handler.TransferItemsTo(reference.Target);
        }
    }
}