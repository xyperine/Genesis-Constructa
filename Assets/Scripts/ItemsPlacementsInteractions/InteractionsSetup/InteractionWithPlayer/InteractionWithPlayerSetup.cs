using GenesisConstructa.ItemsPlacementsInteractions.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer
{
    public abstract class InteractionWithPlayerSetup : MonoBehaviour
    {
        [SerializeField] protected bool interactWithPlayer = true;
        [SerializeField, ShowIf(nameof(interactWithPlayer))] protected PlayerInteractionsSO playerInteractionsSO;
        [SerializeField] protected InteractionTarget target;

        protected abstract InteractionType Type { get; }
        

        private void Awake()
        {
            SetUpInteractionWithPlayer();
        }


        protected void SetUpInteractionWithPlayer()
        {
            if (interactWithPlayer)
            {
                playerInteractionsSO.Register(target, Type);
            }
        }
    }
}