using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public abstract class InteractionsEstablisher : MonoBehaviour
    {
        public abstract bool CanTransferTo(InteractionTarget target);
        public abstract bool CanPickUpFrom(StackZone zone);
    }
}