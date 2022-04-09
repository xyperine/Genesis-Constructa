using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup
{
    public class ConfigurableInteractionWithPlayerSetup : InteractionWithPlayerSetup
    {
        [SerializeField, ShowIf(nameof(interactWithPlayer))] private InteractionType interactionType;

        protected override InteractionType Type => interactionType;
    }
}