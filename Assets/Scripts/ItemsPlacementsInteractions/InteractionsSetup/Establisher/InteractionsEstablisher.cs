using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public abstract class InteractionsEstablisher : MonoBehaviour
    {
        public abstract bool CanTransferTo(InteractionTarget target);
        public abstract bool CanPickUpFrom(StackZone zone);
    }
}