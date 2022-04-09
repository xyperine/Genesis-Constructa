using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public abstract class InteractionsEstablisher : MonoBehaviour
    {
        public abstract bool CanTransferTo(TransferTarget target);
        public abstract bool CanPickUpFrom(StackZone zone);
    }
}