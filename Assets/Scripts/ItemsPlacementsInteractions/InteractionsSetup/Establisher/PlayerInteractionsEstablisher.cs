using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public class PlayerInteractionsEstablisher : InteractionsEstablisher<PlayerInteractionsSO>
    {
        [SerializeField] private InteractionTarget playerZone;
        
        
        private void Awake()
        {
            interactions.SetPlayer(playerZone);
        }


        public override bool CanTransferTo(InteractionTarget target)
        {
            return interactions.CanTransferTo(target);
        }


        public override bool CanPickUpFrom(StackZone zone)
        {
            return interactions.CanPickUpFrom(zone);
        }
    }
}