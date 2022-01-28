using MoonPioneerClone.ItemsInteractions.Transfer;

namespace MoonPioneerClone.Player
{
    public sealed class PlayerTransferBehaviour : TransferStackZoneBehaviour
    {
        protected override bool NeedToBrakeTransfer()
        {
            bool standingStill = true; // Check for movement state here
            return !standingStill;
        }
    }
}