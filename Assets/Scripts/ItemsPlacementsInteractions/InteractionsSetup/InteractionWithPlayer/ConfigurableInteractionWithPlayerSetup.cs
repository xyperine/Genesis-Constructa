using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer
{
    public class ConfigurableInteractionWithPlayerSetup : InteractionWithPlayerSetup
    {
        [SerializeField, ShowIf(nameof(interactWithPlayer))] private InteractionType interactionType;

        protected override InteractionType Type => interactionType;


        public void Setup(PlayerInteractionsSO playerInteractionsSO, InteractionType type)
        {
            interactWithPlayer = true;

            this.playerInteractionsSO = playerInteractionsSO;
            interactionType = type;
        }
    }
}