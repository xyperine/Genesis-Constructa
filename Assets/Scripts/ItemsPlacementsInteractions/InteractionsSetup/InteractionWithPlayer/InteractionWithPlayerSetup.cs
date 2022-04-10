using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer
{
    public abstract class InteractionWithPlayerSetup : MonoBehaviour
    {
        [SerializeField] protected bool interactWithPlayer;
        [SerializeField, ShowIf(nameof(interactWithPlayer))] private PlayerInteractionsSO playerInteractionsSO;

        protected abstract InteractionType Type { get; }
        

        private void Awake()
        {
            SetUpInteractionWithPlayer();            
        }


        private void SetUpInteractionWithPlayer()
        {
            if (interactWithPlayer)
            {
                playerInteractionsSO.Register(GetComponent<InteractionTarget>(), Type);
            }
        }
    }
}