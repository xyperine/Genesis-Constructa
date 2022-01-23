namespace MoonPioneerClone.CollectableItemsInteractions.Split.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<ITransferTarget>
    {
        protected override void InteractWith(ITransferTarget target)
        {
            handler.TransferItemsTo(target);
        }
    }
}