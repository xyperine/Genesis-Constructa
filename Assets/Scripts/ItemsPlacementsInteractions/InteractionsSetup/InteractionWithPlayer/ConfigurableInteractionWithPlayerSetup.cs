using GenesisConstructa.ItemsPlacementsInteractions.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer
{
    public class ConfigurableInteractionWithPlayerSetup : InteractionWithPlayerSetup
    {
        [SerializeField, ShowIf(nameof(interactWithPlayer))] private InteractionType interactionType;

        protected override InteractionType Type => interactionType;


        public void Setup(PlayerInteractionsSO playerInteractionsSO, InteractionType type, InteractionTarget target)
        {
            interactWithPlayer = true;

            this.playerInteractionsSO = playerInteractionsSO;
            interactionType = type;

            this.target = target;
            
            SetUpInteractionWithPlayer();
        }
    }
}