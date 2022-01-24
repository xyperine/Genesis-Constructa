namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public sealed class PlayerTransferBehavior : TransferStackZoneBehavior
    {
        protected override bool NeedToBrakeTransferRoutine()
        {
            bool standingStill = true; // Check for movement state here
            return !standingStill;
        }
    }
}