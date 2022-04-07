using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    public abstract class InteractionsResolver : MonoBehaviour
    {
        public abstract bool CanTransferTo(TransferTarget target);
        public abstract bool CanPickUpFrom(StackZone zone);
    }
}