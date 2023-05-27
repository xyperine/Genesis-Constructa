using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public abstract class InteractionsEstablisher : MonoBehaviour
    {
        public abstract bool CanTransferTo(InteractionTarget target);
        public abstract bool CanPickUpFrom(StackZone zone);
    }
}